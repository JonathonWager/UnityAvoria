using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Fire Ring")]
    public class FireRingAbility : AbilityBase
    {
        public GameObject fireRingPrefab;
        public float duration = 5f;  // Duration the fire ring lasts
        public int level = 1;
        public int useCount = 0;
        public int levelCount = 5;

        public override void Activate(GameObject player)
        {

            if (fireRingPrefab == null)
            {
                Debug.LogError("FireRingAbility: fireRingPrefab is not assigned!");
                return;
            }

            if (player == null)
            {
                Debug.LogError("FireRingAbility: player reference is null!");
                return;
            }

            LevelUp();

            GameObject fireRing = Instantiate(fireRingPrefab, player.transform.position, Quaternion.identity);

            AbilityManager abilityManager = player.GetComponentInChildren<AbilityManager>();
            if (abilityManager == null)
            {
                Debug.LogError("FireRingAbility: AbilityManager not found on player or its children!");
                return;
            }

            abilityManager.StartCoroutine(HandleFireRing(fireRing, player));
        }

        public override void Deactivate(GameObject player)
        {
            // Deactivation logic if needed
        }

        private IEnumerator HandleFireRing(GameObject fireRing, GameObject player)
        {
            yield return new WaitForSeconds(duration);
            Destroy(fireRing);
            EndAbility();  // Signal the end of the ability's effect, triggering the cooldown
        }

        private void LevelUp()
        {
            useCount++;
            if (useCount >= levelCount)
            {
                level++;
                levelCount *= 2;
                // Adjust other properties on level up if needed
            }
        }
    }
}
