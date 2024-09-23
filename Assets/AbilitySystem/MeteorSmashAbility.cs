using System.Collections;
using UnityEngine;
using System.Collections.Generic;
namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Meteor Smash")]
    public class MeteorSmashAbility : AbilityBase
    {   
        [Header("Meteor Base Properties")]
        public float damageBase;
        public float durationBase;
        [Header("Meteor Level Properties")]
        public float damageModifer;
        public float durationModifer;
        [Header("Meteor Properties")]
        public float damage = 10f;
        public GameObject meteorPrefab;
        public float delayBeforeImpact = 0.5f;
        public float duration = 5f;  // How long the meteor stays before it is destroyed
        public int level = 1;
        public int useCount = 0;
        public int levelCount = 10;
        public int levelCountBase = 10;
        public  float minSize,maxSize;
        public int minMeteors,maxMeteors;
        public float meteorSpread;
          public float meteorInterval;

        public override void ResetLevel(){
            level = 1;
            useCount = 0;
            damage = damageBase;
            duration = durationBase;
            levelCount = levelCountBase;
        }
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

       private IEnumerator HandleMeteor(Vector3 cursorPosition, GameObject player)
{
    yield return new WaitForSeconds(delayBeforeImpact);

    // Number of meteors to spawn and the radius for random positions
    int numberOfMeteors = Random.Range(minMeteors, maxMeteors); // Randomize the number of meteors if desired
    List<GameObject> meteors = new List<GameObject>();
    for (int i = 0; i < numberOfMeteors; i++)
    {
        // Random angle and distance within the circle
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(0f, meteorSpread);

        // Calculate spawn position in the circle
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;
        Vector3 spawnPosition = cursorPosition + offset;

        // Instantiate the meteor at the random position
        GameObject meteorObject = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        meteors.Add(meteorObject);
        // Set the meteor's damage and size
        MeteorObject mStats = meteorObject.GetComponent<MeteorObject>();
        mStats.dmgToEnemys = damage;

        // Random size for the meteor
        float rand = Random.Range(minSize, maxSize);
        mStats.size = rand;

        // Wait before the next meteor (optional, to make the spawning look more spread out)
        yield return new WaitForSeconds(meteorInterval);
    }

    // Wait for the meteor duration
    yield return new WaitForSeconds(duration);

    // Destroy meteors after the duration
    foreach (GameObject meteor in meteors)
    {
        Destroy(meteor);
    }

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
                damage += damageModifer;
                duration -= durationModifer;
            }
        }
    }
}
