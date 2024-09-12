using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int wave;
    public int EnemyCount;

    private float elapsed;

    public int totalMaxEnemys = 20;

    private int spawnCount = 0;
    
    public float spawnInterval = 2f;
    public List<GameObject> spawnBoxes;
    
    public GameObject currentArea;
    private bool roundTransition = false;
    public string[] availableEnemies;
    public int[] enemyRatios;
    public float roundTransitionTime = 5f;
     public GameObject UI;
     int test = 0;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        wave = 0;
        EnemyCount = 5;
        InvokeRepeating("checkEnemyCount", 0f, 1f);
        Invoke("UpdateWave", roundTransitionTime);
    }

    void UpdateEnemyCount(){
        //EnemyCount += (int)(-0.2 + 4.7 * wave - 0.1363636 * Mathf.Pow(wave, 2));
        EnemyCount = (int)(14.6619f * Mathf.Log(3.66683f * wave + 10.8431f) - 34.44f);
    }
    void UpdateWave(){
        wave += 1;
        UI.GetComponent<uiUpdater>().updateWave(wave);
        UpdateEnemyCount();
        spawnCount = 0;
        InvokeRepeating("SpawnEnemys", 0f, spawnInterval);
        roundTransition = false;
    }

    void checkEnemyCount(){
        if(!roundTransition){
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy");
            if(enemys.Length <= 0 && spawnCount >= EnemyCount){
                roundTransition = true;
                CancelInvoke("SpawnEnemys");
                Invoke("UpdateWave", roundTransitionTime);
            }
        }
      
    }
    void SpawnEnemys(){
  
        if(spawnCount < EnemyCount){
            test += 1;
            Debug.Log("enemysSpawned " + test + " Wave " + wave);
            int sum = 0;
            for(int i = 0; i < enemyRatios.Length; i++){
                sum += enemyRatios[i];
            }
            int rand = Random.Range(0, sum);
            int counter = sum - enemyRatios[0];
            for(int i = 0; i < availableEnemies.Length; i++){
                if(rand >= counter){
                    currentArea.GetComponent<WaveArea>().spawnEnemy(availableEnemies[i]);
                    spawnCount++;
                    break;
                }else{
                    if( enemyRatios[i + 1] != null){
                        counter -= enemyRatios[i + 1];
                    }
                    
                }
            }            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
