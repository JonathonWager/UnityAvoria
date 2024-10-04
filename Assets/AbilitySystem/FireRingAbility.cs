using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Fire Ring")]
    public class FireRingAbility : AbilityBase
    {
        [Header("Fire Ring Base Properties")]
        public int playerDamageBuffBase = 2;  
        public float enemyDamageTimeIntervalBase = 2f;  
        public int damageBase = 5;  
        public float baseDuration = 5f;
        [Header("Fire Ring Level Up Properties")]
        public float playerBuffModifer;
        public float burnIntervalModifer;
        public float enemyDamageModifer;
        public float durationModifer;
        [Header("Fire Ring Prefab Properties")]
        public int playerDamageBuff = 2;  
        public float enemyDamageTimeInterval = 2f;  
        public int damage = 5;  

        [Header("Fire Ring Manager Properties")]
        public GameObject fireRingPrefab;
        public float duration = 5f; 
        public int useCount = 0;
        public int levelCount = 10;
        public int baseLevelCount = 10;
        public override void ResetLevel(){
            Debug.Log("Reseting LEvel");
            level = 1;
            useCount = 0;
            levelCount = baseLevelCount;
            playerBuffModifer = playerDamageBuffBase;
            enemyDamageTimeInterval = enemyDamageTimeIntervalBase;
            playerDamageBuff = playerDamageBuffBase;
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
                         UpdateLevelUI();
                playerDamageBuff = (int)(playerBuffModifer + playerDamageBuff);
                enemyDamageTimeInterval -= burnIntervalModifer;
                damage = (int)(damage + enemyDamageModifer);
                duration -= durationModifer;
            }
        }
    }
}
