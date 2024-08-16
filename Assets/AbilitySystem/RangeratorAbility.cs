using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Rangerator")]
    public class RangeratorAbility : AbilityBase
    {
        public float rangeModifier = 1.5f;
        public float duration = 5f;
        public int level = 1;
        public int useCount = 0;
        public int levelCount = 5;

        private float originalRange;

        public override void Activate(GameObject player)
        {
            LevelUp();

            var cStats = player.GetComponent<characterStats>();
            if (cStats != null)
            {
                originalRange = cStats.range;
                cStats.range *= rangeModifier;

                player.GetComponentInChildren<AbilityManager>().StartCoroutine(DeactivateAfterTime(player));
            }
        }

        public override void Deactivate(GameObject player)
        {
            var cStats = player.GetComponent<characterStats>();
            if (cStats != null)
            {
                cStats.range = originalRange;
                EndAbility(); // Signal the end of the ability's effect, triggering the cooldown
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
                rangeModifier += 0.1f;
                duration += 0.1f;
            }
        }
    }
}
