using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveArea : MonoBehaviour
{
    public List<GameObject> closestAreas;
    public List<Vector3> vector3List; 

    public void spawnEnemy(string enemyName)
    {
        int randValue = Random.Range(0, 10);
        if (randValue > 4)
        {
            int positionIndex = Random.Range(0, vector3List.Count);
            Vector3 spawnPosition = vector3List[positionIndex];
            if (Resources.Load(enemyName) as GameObject != null)
            {
                Instantiate(Resources.Load(enemyName) as GameObject, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Enemy prefab not found: " + enemyName);
            }
        }
        else
        {
            if (randValue <= 2)
            {
                WaveArea wArea = closestAreas[0].GetComponent<WaveArea>();      
                wArea.spawnEnemy(enemyName);         
            }
            else if (randValue <= 4 && closestAreas.Count >= 2)
            {
                WaveArea wArea = closestAreas[1].GetComponent<WaveArea>();      
                wArea.spawnEnemy(enemyName);   
            }
            
        }
    }
  bool IsPathValid(Vector3 start, Vector3 end)
{
    NavMeshPath path = new NavMeshPath();
    NavMesh.CalculatePath(start, end, NavMesh.AllAreas, path);

    // Check if the path status is PathComplete
    return path.status == NavMeshPathStatus.PathComplete;
}
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("SpawnArea");
            float closestDistance = Mathf.Infinity;
            foreach (GameObject target in targets)
            {
                if (target != this.gameObject)
                {
                    if (IsPathValid(transform.position, target.transform.position))
                    {
                        if(closestAreas.Count == 0){
                            closestAreas.Add(target);
                        }else{
                            float distance = GetNavMeshDistance(transform.position, target.transform.position);
                            int count = closestAreas.Count;
                            for(int i = 0; i < count; i++){
                                if (distance < GetNavMeshDistance(transform.position, closestAreas[i].transform.position)){
                                    closestAreas.Insert(i, target);
                                    break;
                                }
                                if((i == closestAreas.Count - 1)){
                                    closestAreas.Add(target);
                                }
                        }
                        
                        }   
                    }
                }
            }
    }

    // Method to calculate NavMesh distance without using NavMeshAgent
    private float GetNavMeshDistance(Vector3 startPosition, Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(startPosition, targetPosition, NavMesh.AllAreas, path))
        {
            float distance = 0f;
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                for (int i = 1; i < path.corners.Length; i++)
                {
                    distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
                }
            }
            return distance;
        }

        return Mathf.Infinity; // No valid path found
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            closestAreas = new List<GameObject>();
            GameObject[] targets = GameObject.FindGameObjectsWithTag("SpawnArea");
            float closestDistance = Mathf.Infinity;
            foreach (GameObject target in targets)
            {
                if (target != this.gameObject)
                {
                    if (IsPathValid(transform.position, target.transform.position))
                    {
                        if(closestAreas.Count == 0){
                            closestAreas.Add(target);
                        }else{
                            float distance = GetNavMeshDistance(transform.position, target.transform.position);
                            int count = closestAreas.Count;
                            for(int i = 0; i < count; i++){
                                if (distance < GetNavMeshDistance(transform.position, closestAreas[i].transform.position)){
                                    closestAreas.Insert(i, target);
                                    break;
                                }
                                if((i == closestAreas.Count - 1)){
                                    closestAreas.Add(target);
                                }
                        }
                        
                        }   
                    }
                }
            }
            GameObject t = GameObject.FindGameObjectWithTag("WaveManager");
            WaveManager wManager = t.GetComponent<WaveManager>();
            wManager.currentArea = this.gameObject;
        }
    }
}
