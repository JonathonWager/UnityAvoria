using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Fire Ring")]
    public class FireRingAbility : AbilityBase
    {
        [Header("Fire Ring Base Properties")]
        public float playerDamageBuffBase = 2f;  
        public float enemyDamageTimeIntervalBase = 2f;  
        public float damageBase = 5f;  
        public float baseDuration = 5f;
        [Header("Fire Ring Level Up Properties")]
        public float playerBuffModifer;
        public float burnIntervalModifer;
        public float enemyDamageModifer;
        public float durationModifer;
        [Header("Fire Ring Prefab Properties")]
        public float playerDamageBuff = 2f;  
        public float enemyDamageTimeInterval = 2f;  
        public float damage = 5f;  

        [Header("Fire Ring Manager Properties")]
        public GameObject fireRingPrefab;
        public float duration = 5f; 
        public int level = 1;
        public int useCount = 0;
        public int levelCount = 10;
        public int baseLevelCount = 10;
        public override void ResetLevel(){
            level = 1;
            useCount = 0;
            levelCount = baseLevelCount;
            playerBuffModifer = playerDamageBuffBase;
            enemyDamageTimeInterval = enemyDamageTimeIntervalBase;
            damage = damageBase;
            duration = baseDuration;
        }
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
            fireRing frStats = fireRing.GetComponent<fireRing>();
            frStats.playerDamageBuff = playerDamageBuff;
            frStats.enemyDamageTimeInterval = enemyDamageTimeInterval;
            frStats.damage = damage;


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
                playerDamageBuff += playerBuffModifer;
                enemyDamageTimeInterval -= burnIntervalModifer;
                damage += enemyDamageModifer;
                duration -= durationModifer;
            }
        }
    }
}
