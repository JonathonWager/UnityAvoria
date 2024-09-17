using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponsSystem
{
    [CreateAssetMenu(menuName = "Weapon/BasicMelee")]
    public class BasicMelee : WeaponBase
    {
        [Header("Melee Specific Properties")]
        public int damage;
        public float range;
        public float attackAngle;
        public float knockBack;
        public float attackCooldown;

        private bool canAttack = true;
        private LineRenderer lineRenderer; // LineRenderer to show the attack area
        public int arcSegments = 50; // Number of segments for smoothness of the arc
        private Animator animator;
        public override void Attack(GameObject player)
        {
            if (canAttack)
            {
                animator = player.GetComponent<Animator>();
                animator.SetBool("isAttacking", true);
                canAttack = false;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Determine the direction from the character to the mouse
                Vector3 directionToMouse = mousePosition - player.transform.position;
                Vector2 attackDirection = directionToMouse.normalized; // Normalize to ensure direction only

                // Draw the attack area using LineRenderer
                DrawAttackArc(player, attackDirection);

                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player.transform.position, range);
                foreach (Collider2D collider in hitColliders)
                {
                    if (collider.CompareTag("enemy")) // Check if the collider has the correct tag
                    {
                        // Determine the direction from the player to the target
                        Vector3 directionToTarget = (collider.transform.position - player.transform.position).normalized;

                        // Calculate the angle between the attack direction and the target direction
                        float angleToTarget = Vector2.Angle(attackDirection, directionToTarget);

                        // Check if the target is within the attack angle
                        if (angleToTarget <= attackAngle / 2f)
                        {
                            Vector2 knockbackDirection = directionToTarget;
                            collider.GetComponent<enemyStats>().takeDamage(damage, knockbackDirection, knockBack);
                        }
                    }
                }

                // Start cooldown and clear the attack arc
                player.GetComponent<MonoBehaviour>().StartCoroutine(Cooldown());
                player.GetComponent<MonoBehaviour>().StartCoroutine(ClearAttackArc(0.5f)); // Clear the arc after a short delay
            }
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(attackCooldown);
            attackReset();
        }

        void attackReset()
        {
            canAttack = true;
        }
    
        void OnEnable()
        {
            
            canAttack = true;
            weaponClass = WeaponClass.Melee;
        }

        // Method to draw the attack arc using LineRenderer
        private void DrawAttackArc(GameObject player, Vector2 attackDirection)
        {
            if (lineRenderer == null)
            {
                // Create and configure the LineRenderer if not already created
                lineRenderer = player.AddComponent<LineRenderer>();
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.positionCount = arcSegments + 1;
                lineRenderer.useWorldSpace = true;
                lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Default material
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }

            float halfAttackAngle = attackAngle / 2f;
            float angleStep = attackAngle / arcSegments;

            Vector3 playerPosition = player.transform.position;
            lineRenderer.positionCount = arcSegments + 1;

            for (int i = 0; i <= arcSegments; i++)
            {
                // Calculate the angle for each point in the arc
                float currentAngle = -halfAttackAngle + (i * angleStep);
                Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);

                // Rotate the attackDirection vector and scale by range
                Vector3 rotatedDirection = rotation * attackDirection.normalized; // Normalize ensures direction stays constant
                Vector3 arcPoint = playerPosition + rotatedDirection * range; // Scale by range only

                // Set position in the LineRenderer
                lineRenderer.SetPosition(i, arcPoint);
            }
        }

        // Coroutine to clear the attack arc after a short delay
        private IEnumerator ClearAttackArc(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 0; // Hide the LineRenderer
            }
        }
    }
}
