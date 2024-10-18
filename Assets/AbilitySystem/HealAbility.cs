using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Heal")]
    public class HealAbility : AbilityBase
    {
        [Header("Heal Base Params")]
        public int baseHealAmount;
        public float baseRadius;
        public float baseInterval;
        public float baseTime;
        public float baseCooldown;
        [Header("Heal Level Up Properties")]
        public int healIncrease;
        public float radiusIncrease;
        public float intervalDecrease;
        public float timeIncrease; 
         public float cooldownDecrease; 
         [Header("Heal Ability Other")]
         public GameObject healCircle;

        int healAmount;
        float radius;
        float interval;
        float time; 

        public int useCount = 0;  
        public int baseLevelCount = 10;
        public int levelCount = 10;
        public override void ResetLevel(){
            levelCount = baseLevelCount;
            useCount = 0;
            level = 1;
            healAmount=baseHealAmount;
            radius = baseRadius;
            interval = baseInterval;
            time = baseTime;
            cooldown = baseCooldown;

        }
        public override void Activate(GameObject player)
        {
            GameObject hCircle = Instantiate(healCircle, player.transform.position, Quaternion.identity);
            hCircle.GetComponent<HealCircle>().SetStats(radius,healAmount,interval,time);
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
                healAmount += healIncrease;
                radius += radiusIncrease;
                interval -= intervalDecrease;
                time += timeIncrease;
                cooldown -= cooldownDecrease;
            }
        }
    }
}
