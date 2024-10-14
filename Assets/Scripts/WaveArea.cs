using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DropBuffs;
public class WaveArea : MonoBehaviour
{
    public List<GameObject> closestAreas;
    public List<Vector3> vector3List; 
    private Dictionary<string, GameObject> enemyPrefabs = new Dictionary<string, GameObject>();
    DropBase[] allDrops; 
     GameObject goldPrefab;
    public void spawnEnemy(string enemyName, int healthBuff, int dmgBuff ,GameObject parent)
    {
        int randValue;
        if(closestAreas.Count >= 1){
            randValue = Random.Range(0, 10);
        }else
        {
            randValue = 5;
        }
       
        if (randValue > 4)
        {
            int positionIndex = Random.Range(0, vector3List.Count);
            Vector3 spawnPosition = vector3List[positionIndex];
             if (enemyPrefabs.TryGetValue(enemyName, out GameObject enemyPrefab))
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemyStats eStats = enemy.GetComponent<enemyStats>();
                eStats.hp += healthBuff; 
                eStats.dmgBuff += dmgBuff; 
                eStats.allDrops = allDrops;
                eStats.gold = goldPrefab;
            }
            else
            {
                Debug.LogError("Enemy prefab not found: " + enemyName);
            }
        }
        else
        {
            
            if(closestAreas.Count < 2){
                randValue = 2;
            }
            if (randValue <= 2&& closestAreas.Count >= 1)
            {
                if(closestAreas[0] == parent && closestAreas[1] != null ){
                    WaveArea wArea = closestAreas[1].GetComponent<WaveArea>();      
                    wArea.spawnEnemy(enemyName, healthBuff,dmgBuff,this.gameObject);         
                }else{
                    WaveArea wArea = closestAreas[0].GetComponent<WaveArea>();      
                    wArea.spawnEnemy(enemyName, healthBuff,dmgBuff,this.gameObject);    
                }
                
            }
            else if (randValue <= 4 && closestAreas.Count >= 2)
            {
                if(closestAreas[1] == parent && closestAreas[2] != null ){
                    WaveArea wArea = closestAreas[1].GetComponent<WaveArea>();      
                    wArea.spawnEnemy(enemyName, healthBuff,dmgBuff,this.gameObject);         
                }else if(closestAreas[1] == parent){
                    WaveArea wArea = closestAreas[0].GetComponent<WaveArea>();      
                    wArea.spawnEnemy(enemyName, healthBuff,dmgBuff,this.gameObject);    
                }else{
                    WaveArea wArea = closestAreas[1].GetComponent<WaveArea>();      
                    wArea.spawnEnemy(enemyName,healthBuff,dmgBuff,this.gameObject);   
                }
             
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
        allDrops= Resources.LoadAll<DropBase>("Drops");
        GameObject[] loadedEnemies = Resources.LoadAll<GameObject>("Enemies");
        goldPrefab = Resources.Load<GameObject>("Gold");
        foreach (GameObject enemy in loadedEnemies)
        {
            enemyPrefabs.Add(enemy.name, enemy);
        }

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
