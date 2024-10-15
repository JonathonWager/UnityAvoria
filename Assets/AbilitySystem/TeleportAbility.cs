using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Teleport")]
    public class TeleportAbility : AbilityBase
    {
        [Header("Warp Base Params")]
        public float baseRange;
        public float baseSpeed;
        public float baseCooldown = 5f;
        
        [Header("Warp Level Up Properties")]
        public float rangeIncrease = 0.5f;
        public float speedIncrease = 0.5f;
        public float cooldownDecrease = 0.25f;

         [Header("Warp Level Data Properties")]
        public int levelCount = 10;
        public int useCount = 0;  
        public int baseLevelCount = 10;
        float range;
        float teleportSpeed;
        bool firstPress = false;   
        [Header("Other Warp Properties")]
        public float targetResetTimer = 3f;

        public GameObject targetCursor;
        GameObject targetInUse;
         float maxDistance = 1.0f; // Maximum distance to sample from the position

        public Transform cameraTransform;
        private Vector2 targetPosition;
        AbilityManager abilityManager ;
        characterStats cStats;
        public override void ResetLevel(){
            firstPress = false;
            range = baseRange;
            teleportSpeed = baseSpeed;
            levelCount = baseLevelCount;
            useCount = 0;
            level = 1;
            cooldown = baseCooldown;
            cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;       
        }
        public override void Activate(GameObject player)
        {
            AbilityManager abilityManager = player.GetComponentInChildren<AbilityManager>();
            if(!firstPress){
                firstPress = true;
                targetInUse = Instantiate(targetCursor, player.transform.position, Quaternion.identity);
                targetInUse.GetComponent<TeleportTarget>().range = range;
                cooldown= 0f;

                abilityManager.StartCoroutine(ResetTarget());
            }else{
               if(targetInUse.GetComponent<TeleportTarget>().isValid){
                    
                    cStats = player.GetComponent<characterStats>();
                    cStats.Teleport();
                    targetPosition =  targetInUse.transform.position;
                    Destroy(targetInUse);
                    //cameraTransform.gameObject.GetComponent<camFollow>().pauseMovement = true;
                    abilityManager.StartCoroutine(TeleportCoroutine(player));
                    
                    firstPress = false;
                    cooldown = baseCooldown;
                    LevelUp();
               }          
            }
        }
        private IEnumerator TeleportCoroutine(GameObject player)
        {
            Vector3 playerStartLocation = player.transform.position;
            Vector3 playerEndPosition = new Vector3(targetPosition.x, targetPosition.y, 0f);
            float distance = Vector3.Distance(playerStartLocation, playerEndPosition);
            float startTime = Time.time;

            // Start moving the camera
            while (Vector3.Distance(player.transform.position, playerEndPosition) > 0.01f) // Using a small threshold instead of direct equality
            {
                Debug.Log("TRUING TO MOVE CAM");
                float traveledDistance = (Time.time - startTime) * teleportSpeed;
                float journeyFraction = traveledDistance / distance;
                
                //cameraTransform.position = Vector3.Lerp(cameraStartPosition, cameraEndPosition, journeyFraction);
                player.transform.position = Vector3.Lerp(playerStartLocation, playerEndPosition, journeyFraction);
                yield return null;
            }
            
            // Ensure the camera reaches the exact target position
           // cameraTransform.position = cameraEndPosition;

            // Update player's position to the target
           // player.transform.position = new Vector3(targetPosition.x, targetPosition.y, player.transform.position.z);
            
            // Stop teleport effects on player and resume camera follow
            cStats.StopTeleport();
           // cameraTransform.gameObject.GetComponent<camFollow>().pauseMovement = false;
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

        private void LevelUp()
        {
            useCount++;
            if(useCount >= levelCount){
                useCount = 0;
                level++;
                levelCount =(int)(levelCount * 1.5);
                UpdateLevelUI();
                range += rangeIncrease;
                teleportSpeed += speedIncrease;
                cooldown -= cooldownDecrease;
            }
        }
    }
}