using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitySystem
{
       [CreateAssetMenu(menuName = "Abilities/Teleport")]
    public class TeleportAbility : AbilityBase
    {
        bool firstPress = false;   
        public float baseCooldown = 5f;
            public float targetResetTimer = 3f;

        public GameObject targetCursor;
        GameObject targetInUse;
         float maxDistance = 1.0f; // Maximum distance to sample from the position
         
        public override void ResetLevel(){
            firstPress = false;
        }
        public override void Activate(GameObject player)
        {
            if(!firstPress){
                firstPress = true;
                targetInUse = Instantiate(targetCursor, player.transform.position, Quaternion.identity);
                cooldown= 0f;
                 AbilityManager abilityManager = player.GetComponentInChildren<AbilityManager>();
                if (abilityManager == null)
                {
                    Debug.LogError("FireRingAbility: AbilityManager not found on player or its children!");
                    return;
                }

                abilityManager.StartCoroutine(ResetTarget());
            }else{
               if(targetInUse.GetComponent<TeleportTarget>().isValid){
                player.transform.position = targetInUse.transform.position;
                  Destroy(targetInUse);
                firstPress = false;
                cooldown= baseCooldown;
               }
              
            
                
            }
        }
        
        public override void Deactivate(GameObject player)
        {
            // Deactivation logic if needed
        }
        private IEnumerator ResetTarget(){
            yield return new WaitForSeconds(targetResetTimer);
            if(firstPress){
                firstPress = false;
                Destroy(targetInUse);
            }
        }
    }
}