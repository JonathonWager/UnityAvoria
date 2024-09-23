using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeaponsSystem{
    [CreateAssetMenu(menuName = "Weapon/BasicRanged")]
    public class BasicRanged : WeaponBase
    {
        [Header("Ranged Specific Properties(Base)")]
        public float baseFireRate;
        public float baseDamage;
        public float baseSpeed;
        public float baseRange;
        public int baseLevelInc;
        [Header("CharacterStats buffs")]
        public int playerDamageBuff;
        public float playerRangeBuff;  
        
        [Header("Ranged Object Specific Properties")]
        public float fireRate;

        public string projectilePrefabName;
        private GameObject projectile;
        private bool canFire = true;

      [Header("Ranged Specific Properties(Actual)")]
        public float damage;
        public float speed;
        public float range;

        public float knockBack;

        public float safteyDestoryTime;
        public float levelUpDmgBuff ;
         public float levelUpRangeBuff ;
        public float levelUpCooldownBuff ;

        void getPlayerBuffs(GameObject player){
            characterStats cStats = player.GetComponent<characterStats>();
            playerDamageBuff = cStats.dmgBuff;
            playerRangeBuff = cStats.rangeBuff;
        }
        public override void ResetLevel(){
            damage = baseDamage;
            speed = baseSpeed;
            range = baseRange;
            fireRate = baseFireRate;
            level = 1;
            levelInc = baseLevelInc;
            useCount = 0;
        }
        public override void Attack(GameObject player)
        {
            if (canFire)
            {
                getPlayerBuffs(player);
                GameObject instantProjectile = Instantiate(projectile, player.transform.position, Quaternion.identity);
                Debug.Log(instantProjectile.name);
                BasicProjectile proStats = instantProjectile.GetComponent<BasicProjectile>();
                proStats.dmg = (int)(damage + playerDamageBuff);
                proStats.speed = speed;
                proStats.range = (range + playerDamageBuff);
                proStats.knockBack = knockBack;
                proStats.deleteTime = safteyDestoryTime;
                
                canFire = false;

                // Set a cooldown for firing
                player.GetComponent<MonoBehaviour>().StartCoroutine(Cooldown());
            }
        }
        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(fireRate);
            FireReset();
        }
        void FireReset()
        {
            canFire = true;
        }
        // Start is called before the first frame update
        void OnEnable()
        {
            canFire = true;
            projectile = Resources.Load(projectilePrefabName) as GameObject;
        }
        public override void CheckLevel(){
            useCount++;
            if(useCount >= levelInc){
                level++;
                   UpdateLevelUI();
                levelInc = levelInc * 2;
                damage = damage + levelUpDmgBuff;
                range = range +  levelUpRangeBuff;
                fireRate = fireRate - levelUpCooldownBuff;
            }
        }
    }
}


