using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Meteor Smash")]
    public class MeteorSmashAbility : AbilityBase
    {
        public GameObject meteorPrefab;
        public float delayBeforeImpact = 0.5f;
        public float duration = 5f;  // How long the meteor stays before it is destroyed
        public int level = 1;
        public int useCount = 0;
        public int levelCount = 5;

        public override void Activate(GameObject player)
        {
            if (meteorPrefab == null)
            {
                Debug.LogError("MeteorSmashAbility: meteorPrefab is not assigned!");
                return;
            }

            if (player == null)
            {
                Debug.LogError("MeteorSmashAbility: player reference is null!");
                return;
            }

            if (Camera.main == null)
            {
                Debug.LogError("MeteorSmashAbility: Main Camera not found!");
                return;
            }

            LevelUp();

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            AbilityManager abilityManager = player.GetComponentInChildren<AbilityManager>();
            if (abilityManager != null)
            {
                abilityManager.StartCoroutine(HandleMeteor(worldPosition, player));
            }
        }

        public override void Deactivate(GameObject player)
        {
            // Intentionally left blank, as the coroutine will handle the lifecycle
        }

        private IEnumerator HandleMeteor(Vector3 position, GameObject player)
        {
            yield return new WaitForSeconds(delayBeforeImpact);

            GameObject meteor = Instantiate(meteorPrefab, position, Quaternion.identity);
            Debug.Log("Meteor instantiated at position: " + position);

            yield return new WaitForSeconds(duration);
            Destroy(meteor);
            Debug.Log("Meteor destroyed after duration.");

            // Trigger cooldown via AbilityManager
            EndAbility();
        }

        private void LevelUp()
        {
            useCount++;
            if (useCount >= levelCount)
            {
                level++;
                levelCount *= 2;
                // Adjust other properties on level up if needed
            }
        }
    }
}
