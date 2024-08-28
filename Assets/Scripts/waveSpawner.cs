using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    bool entered = false;
    bool startedPoolingFlag = false;

    public string[] availableEnemies; // List of enemy prefab names available for random selection
    public List<Vector3> vector3List; // List of spawn positions
    public int enemyCountToSpawn = 5; // Number of enemies to spawn

    void SpawnEnemies()
    {
  
        for (int i = 0; i < enemyCountToSpawn; i++)
        {
            // Randomly select an enemy type
            int enemyIndex = Random.Range(0, availableEnemies.Length);
            string selectedEnemy = availableEnemies[enemyIndex];

            // Randomly select a spawn position
            int positionIndex = Random.Range(0, vector3List.Count);
            Vector3 spawnPosition = vector3List[positionIndex];

            // Spawn the enemy at the selected position
            if (Resources.Load(selectedEnemy) as GameObject != null)
            {
                Instantiate(Resources.Load(selectedEnemy) as GameObject, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Enemy prefab not found: " + selectedEnemy);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            entered = true;
        }
    }

    void Update()
    {
        if (entered && !startedPoolingFlag)
        {
            SpawnEnemies();
            startedPoolingFlag = true;
        }
    }
}
