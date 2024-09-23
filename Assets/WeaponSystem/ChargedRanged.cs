using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeaponsSystem{
    [CreateAssetMenu(menuName = "Weapon/ChargedRanged")]
    public class ChargedRanged : WeaponBase
    {
        [Header("Charged Ranged Properties(Base)")]
        public float maxDamageBase;
        public float maxSpeedBase;

        public float maxRangeBase;
        public float knockBackBase;
        public int baseLevelInc;
        public float maxChargeBase;
        [Header("Charge Properties")]
        public float chargeTime;

        public float maxCharge;

        private float power;
        [Header("Projectile Properties")]

        public float maxDamage;
        public float maxSpeed;

        public float maxRange;
        public float knockBack;
        public int collateralCount;
        public string projectilePrefabName;
        private GameObject projectile;
        public float safteyDestoryTime;
        [Header("CharacterStats buffs(DO NOT SET)")]
        public int playerDamageBuff;
        public float playerRangeBuff;  
        
        [Header("Level buffs")]
         public float levelUpDmgBuff ;
         public float levelUpRangeBuff ;
        public float levelUpChargeBuff ;
        public float levelUpSpeedBuff ;
        public float levelKnockbackBuff ;
        public override void ResetLevel(){
            maxDamage = maxDamageBase;
            maxSpeed = maxSpeedBase;
            maxRange = maxRangeBase;
            knockBack = knockBackBase;
            level = 1;
            useCount = 0;
            maxCharge = maxChargeBase;
        }

        public override void Attack(GameObject player){
            SetChargeAmount();
            Debug.Log("Right mouse button held for: " + power + " seconds");
            GameObject instantProjectile = Instantiate(projectile, player.transform.position, Quaternion.identity);
            BasicProjectile proStats = instantProjectile.GetComponent<BasicProjectile>();
            proStats.dmg = (int)((maxDamage + playerDamageBuff)* power);
            proStats.speed = maxSpeed * power;
            proStats.range = (maxRange + playerDamageBuff) * power;
            proStats.knockBack = knockBack * power;
            proStats.deleteTime = safteyDestoryTime;
            proStats.collateralCount = (int)(collateralCount * power);
        }

        public void SetChargeAmount(){
            if(chargeTime > maxCharge){
                power = 1;
            }else{
                power = (chargeTime/maxCharge);
            }
           
        }
        public override void CheckLevel()
        {
            useCount++;
            if(useCount >= levelInc){
                level++;
                   UpdateLevelUI();
                levelInc = levelInc * 2;
                maxDamage += levelUpDmgBuff;
                knockBack += levelKnockbackBuff;
                maxRange += levelUpRangeBuff;
                maxCharge -= levelUpChargeBuff;
                maxSpeed += levelUpSpeedBuff;
            }
        }
           void OnEnable()
        {
            projectile = Resources.Load(projectilePrefabName) as GameObject;
        }
    }
}