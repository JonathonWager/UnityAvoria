using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Scatter")]
    public class ScatterShotAbility : AbilityBase
    {
        [Header("ScatterShot Base Params")]
        public int baseShotCount = 1;
        public float baseShotSpeed = 5f;
        public float baseCooldownTimer = 12f;
        public int baseDamage = 5;
        public int baseCollateralCount = 1;

        [Header("ScatterShot Level Up Properties")]
        public int shotCountIncrease = 1;
        public float shotSpeedIncrease = 1f;
        public float cooldownDecrease = 0.5f;
        public int collateralIncrease = 1;
        public int damageIncrease = 5;

        int shotCount;
        float shotSpeed;
        int damage;
        int collateralCount;

        public int useCount = 0;  
        public int baseLevelCount = 10;
        public int levelCount = 10;


        public GameObject scatterShot;
        public override void ResetLevel(){
            levelCount = baseLevelCount;
            useCount = 0;
            level = 1;
            cooldown = baseCooldownTimer;
            damage = baseDamage;
            shotCount = baseShotCount;
            shotSpeed = baseShotSpeed;
            collateralCount = baseCollateralCount;
 
        }
        public override void Activate(GameObject player)
        {
            Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorWorldPosition.z = 0;

            // Calculate the initial direction from the player to the cursor (target)
            Vector2 direction = (cursorWorldPosition - player.transform.position).normalized;

            // Calculate the base angle and increment between each shot
            float baseAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)-90f; 
            float angleInc = 360f / shotCount;

            for (int i = 0; i < shotCount; i++)
            {
                // Calculate the current angle for this shot
                float currentAngle = baseAngle + (i * angleInc);

                // Convert the angle to a Quaternion rotation
                Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);

                // Instantiate the scatter shot at the player's position with the calculated rotation
                GameObject shot = Instantiate(scatterShot, player.transform.position, rotation);
                ScatterShot shotScript = shot.GetComponent<ScatterShot>();
                shotScript.damage = damage;
                shotScript.speed = shotSpeed;
                shotScript.collateralCount = collateralCount;
            }
            LevelUp();
        }
        public override void Deactivate(GameObject player)
        {

        }
        private void LevelUp()
        {
            useCount++;
            if(useCount >= levelCount){
                useCount = 0;
                level++;
                levelCount = (int)(levelCount * 1.5);
                UpdateLevelUI();
                shotCount += shotCountIncrease;
                shotSpeed += shotSpeedIncrease;
                cooldown -= cooldownDecrease;
                damage += damageIncrease;
            }
        }
    }
}
