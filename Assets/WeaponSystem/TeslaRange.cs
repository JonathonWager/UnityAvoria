using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeaponsSystem{
    [CreateAssetMenu(menuName = "Weapon/TeslaRange")]
    public class TeslaRange : WeaponBase
    {
        [Header("Ranged Specific Properties(Base)")]
        public float baseFireRate;
        public float baseDamage;
        public float baseSpeed;

        public int baseLevelInc;

        public int baseMinJumps;
        public int baseMaxJumps;
        public float baseJumpOddsDivisor;
        public float baseJumpOdds; 
        [Header("CharacterStats buffs")]
        public int playerDamageBuff;
        public float playerRangeBuff;  
        
        [Header("Ranged Object Specific Properties")]
        public float fireRate;

        public string projectilePrefabName;
        private GameObject projectile;
        public bool canFire = true;
        public float cooldownElapsedTime = 0f;
        private bool isCooldownActive = false;
        public float safteyDestoryTime;
      [Header("Ranged Specific Properties(Actual)")]
        public float damage;
        public float speed;

        public float knockBack;

        int minJumps,maxJumps;
        float jumpOddsDivisor,jumpOdds;
        
        [Header("Tesla Level Up Properties")]
        public float levelUpDmgBuff ;

        public float levelUpCooldownBuff ;
        public float levelUpSpeedBuff ;

        public float levelUpJumpOddsDivisor;
        public float jumpOddsIncrease;

        
      

        void getPlayerBuffs(GameObject player){
            characterStats cStats = player.GetComponent<characterStats>();
            playerDamageBuff = cStats.dmgBuff;
            playerRangeBuff = cStats.rangeBuff;
        }
        public override void ResetLevel(){
            damage = baseDamage;
            speed = baseSpeed;
            fireRate = baseFireRate;
            level = 1;
            levelInc = baseLevelInc;
            useCount = 0;
            minJumps = baseMinJumps;
            maxJumps = baseMaxJumps;
            jumpOddsDivisor = baseJumpOddsDivisor;
            jumpOdds = baseJumpOdds;
        }
        public override void Attack(GameObject player)
        {
            if (canFire)
            {
                getPlayerBuffs(player);
                GameObject instantProjectile = Instantiate(projectile, player.transform.position, Quaternion.identity);
                Debug.Log(instantProjectile.name);
                TeslaBullet proStats = instantProjectile.GetComponent<TeslaBullet>();
                proStats.minJumps = minJumps;
                proStats.maxJumps = maxJumps;
                proStats.jumpOddsDivisor = jumpOddsDivisor;
                proStats.jumpOdds = jumpOdds;
                proStats.damage = damage;
                proStats.speed = speed;
                
                canFire = false;

                // Set a cooldown for firing
                player.GetComponent<MonoBehaviour>().StartCoroutine(Cooldown());
            }
        }
     
         private IEnumerator Cooldown()
        {
        isCooldownActive = true;
        cooldownElapsedTime = 0f;

        while (cooldownElapsedTime < fireRate)
        {
            cooldownElapsedTime += Time.deltaTime;
            // Optionally, update a UI element here to show the progress
            yield return null; // Waits until the next frame
        }

             FireReset();  // Reset the attack after cooldown finishes
        isCooldownActive = false;
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
                fireRate = fireRate - levelUpCooldownBuff;
                speed += levelUpSpeedBuff;
                if (level % 3 == 0)
                {
                    minJumps++;
                }
                if (level % 2 == 0)
                {
                    maxJumps++;
                    if(jumpOddsDivisor + levelUpJumpOddsDivisor < 1){
                        jumpOddsDivisor += levelUpJumpOddsDivisor;
                    }
                    if(jumpOdds + jumpOddsIncrease < 1){
                        jumpOdds += jumpOddsIncrease;
                    }else{
                        jumpOdds = 1;
                    }


                }               
            }
        }
    }
}


