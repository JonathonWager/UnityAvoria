using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Speederator")]
    public class SpeederatorAbility : AbilityBase
    {
        [Header("Speederator Base Properties")]
         public float speedModifierBase;
        public float durationBase;
        [Header("Speederator Level Properties")]
         public float speedLevelModifier;
        public float durationModifer;
        [Header("Speederator Properties")]
        public float speedModifier = 1.5f;
        public float duration = 5f;
        public int level = 1;
        public int useCount = 0;
        public int levelCount = 10;

        private float originalSpeed;
        public int levelCountBase;
        private Coroutine activeCoroutine;  // To track the coroutine
        public override void ResetLevel(){
            speedModifier = speedModifierBase;
            duration = durationBase;
            level = 1;
            useCount = 0;
            levelCount = levelCountBase;
        }
        public override void Activate(GameObject player)
        {
            LevelUp();

            var mStats = player.GetComponent<playerMovement>();
            if (mStats != null)
            {
                if (activeCoroutine != null)
                {
                    // Stop any existing coroutine
                    player.GetComponentInChildren<AbilityManager>().StopCoroutine(activeCoroutine);
                    Debug.Log("Stopped previous coroutine before activating Speederator.");
                }

                // Store the original speed and apply the speed boost
                originalSpeed = mStats.moveSpeed;
                mStats.moveSpeed += speedModifier;
                Debug.Log($"Speed increased to {mStats.moveSpeed} for {duration} seconds.");

                // Start the coroutine to deactivate after the duration
                activeCoroutine = player.GetComponentInChildren<AbilityManager>().StartCoroutine(DeactivateAfterTime(player));
            }
            else
            {
                Debug.LogError("SpeederatorAbility: playerMovement component not found on the player!");
            }
        }

        public override void Deactivate(GameObject player)
        {
            var mStats = player.GetComponent<playerMovement>();
            if (mStats != null)
            {
                // Restore the original speed
                mStats.moveSpeed = originalSpeed;
                Debug.Log($"Speed reset to original value: {mStats.moveSpeed}");
                EndAbility();  // Signal the end of the ability's effect, triggering the cooldown
            }
            else
            {
                Debug.LogError("SpeederatorAbility: playerMovement component not found on the player during deactivation!");
            }
        }

        private IEnumerator DeactivateAfterTime(GameObject player)
        {
            yield return new WaitForSeconds(duration);
            Deactivate(player);
        }

        private void LevelUp()
        {
            useCount++;
            if (useCount >= levelCount)
            {
                level++;
                levelCount *= 2;
                speedModifier += speedLevelModifier;
                duration += durationModifer;
            }
        }
    }
}
