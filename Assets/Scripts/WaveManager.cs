          using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    TrapRoomManager trapRoomManager;
    BeastManager beastManager;
    public int wave;
    public int EnemyCount;

    private float elapsed;

    public int totalMaxEnemys = 20;

    private int spawnCount = 0;
    
    public float spawnInterval = 2f;
    public List<GameObject> spawnBoxes;
    
    public GameObject currentArea;
    public bool roundTransition = true;
    public string[] availableEnemies;
    public int[] enemyRatios;
    public float roundTransitionTime = 5f;
    public float baseRoundTransitionTime = 15f;
     public GameObject UI;


     public int waveShopResetWave = 3;
     private int waveShopCount = 0;
     int countdownTimer;
     bool skipWave = false;

     public int spawnSizeInterval = 2;
     public int spawnSizeIncrease = 1;
     public int spawnSize = 1;

     private int spawnSizeCounter = 0;

     public int enemyHealthBuffBase = 10;
     public int enemyHealthBuffTotal = 0;
     public int enemyDamageBuffBase = 10;
     public int enemyDamageBuffTotal = 0;
     public float potionIncreaseMofider = 1.1f;
     public float potionIncreaseAmount = 0.1f;

     int enemyBuffCounter = 0;
     public int enemyBuffRoundInterval = 3;
     bool isPaused = false;

    int beastRoomCounter = 0;
    bool beastStarted = false;
    public int beastRoomStartRound = 10;
    public int beastRoomSpawnInterval = 5;
    bool beastsSpawned = false;
    float beastSpeedBuff = 0;
    public float beastSpeedBuffIncrease = 0.5f;
    public float skipTimeScale = 15f;

    // Start is called before the first frame update
    void updatePotionStats(){
        potionIncreaseMofider += potionIncreaseAmount;
    }
    public void PauseSpawning(){
        CancelInvoke("SpawnEnemys");
        isPaused = true;
    }
    public void UnPauseSpawning(){
        isPaused = false;
        InvokeRepeating("SpawnEnemys", 0f, spawnInterval);

    }
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        trapRoomManager = GameObject.FindGameObjectWithTag("TrapManager").GetComponent<TrapRoomManager>();
        beastManager = GameObject.FindGameObjectWithTag("BeastManager").GetComponent<BeastManager>();
        wave = 0;
        EnemyCount = 5;
        roundTransitionTime = baseRoundTransitionTime;
        InvokeRepeating("checkEnemyCount", 0f, 1f);

        StartCoroutine(CountdownCoroutine(roundTransitionTime));
    }
    void updateEnemyStats(){
        enemyHealthBuffTotal += enemyHealthBuffBase;
        enemyDamageBuffTotal += enemyDamageBuffBase;

        GameObject.FindGameObjectWithTag("TrapManager").GetComponent<TrapRoomManager>().UpdateChildrenDmg(enemyDamageBuffBase);
    }
    void UpdateEnemyCount(){
        //EnemyCount += (int)(-0.2 + 4.7 * wave - 0.1363636 * Mathf.Pow(wave, 2));
        EnemyCount = (int)(0.33 * (wave * wave) +5);
    }
    void UpdateShops(){
        GameObject shopManager = GameObject.FindGameObjectWithTag("shopmanager");
        shopManager.GetComponent<ShopManager>().DeleteShops();
    }
    void UpdateWave(){
        UI.GetComponent<uiUpdater>().DisableWaveAccept();
        wave += 1;
        GameObject.FindGameObjectWithTag("BuffShop").GetComponent<BuffShopSpawn>().UpdateShop();
        enemyBuffCounter += 1;
        spawnSizeCounter += 1;
        waveShopCount += 1;
        beastRoomCounter += 1;
        if(!beastStarted){
            if(beastRoomCounter == beastRoomStartRound){
                beastStarted = true;
                beastRoomCounter = beastRoomSpawnInterval;
            }
        }
        if(beastRoomCounter == beastRoomSpawnInterval && beastStarted){
            beastRoomCounter = 0;
            beastManager.SpawnBeasts(wave,beastSpeedBuff,enemyHealthBuffTotal, enemyDamageBuffTotal);
            beastsSpawned = true;
        }
        if(enemyBuffCounter == enemyBuffRoundInterval){
            enemyBuffCounter = 0;
            updateEnemyStats() ;
            updatePotionStats();
            beastSpeedBuff += beastSpeedBuffIncrease;
        }

        if(waveShopCount == waveShopResetWave){
            waveShopCount  = 0;
            UpdateShops();
        }
        if(spawnSizeCounter == spawnSizeInterval){
            spawnSizeCounter = 0;
            spawnSize += spawnSizeIncrease;
        }
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

                skipWave = false;
                if(beastsSpawned){
                    beastsSpawned = false;
                    beastManager.DeleteBeasts();
                }
                StartCoroutine(CountdownCoroutine(roundTransitionTime));
                
            
            }
        }
      
    }
    void skipHeal(){
        GameObject player = GameObject.FindGameObjectWithTag("character");
        int healing = player.GetComponent<characterStats>().regenAmount * (int)(countdownTimer/player.GetComponent<characterStats>().regenTime);
        if(player.GetComponent<characterStats>().hp < player.GetComponent<characterStats>().basehp){
            if(healing + player.GetComponent<characterStats>().hp >= player.GetComponent<characterStats>().basehp){
                player.GetComponent<characterStats>().hp = player.GetComponent<characterStats>().basehp;
            }else{
                player.GetComponent<characterStats>().hp += healing;
            }
        }
    }
    IEnumerator CountdownCoroutine(float roundTransitionTime){
        countdownTimer = (int)(roundTransitionTime);

        // Update the countdown each second
        playerMovement mStats = GameObject.FindGameObjectWithTag("character").GetComponent<playerMovement>();
        while (countdownTimer > 0){
            if (skipWave)
            {
                Time.timeScale = skipTimeScale;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                mStats.stopMovement();
                skipWave = false;
                //skipHeal();
                //countdownTimer = 0;
            }
            UI.GetComponent<uiUpdater>().nextWaveUI(countdownTimer, (wave + 1)); // Update your UI with the current countdown
            if(!isPaused){
                    UI.GetComponent<uiUpdater>().EnableWaveAccept();
            }
            yield return new WaitForSeconds(1f);
            countdownTimer--;
        }
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

                mStats.enabled = true;
                mStats.startAnimator();
        // After countdown finishes, proceed to the next wave
        if(!isPaused){
                UpdateWave();
        }

    }
    void SpawnEnemys(){
        UnityEngine.Profiling.Profiler.BeginSample("SpawnEnemys Logic");
        int currentSpawnSize = 0;
        if(spawnSize + spawnCount > EnemyCount){
            currentSpawnSize = (spawnSize + spawnCount) - EnemyCount;
        }else{
            currentSpawnSize = spawnSize;
        }
        if(spawnCount < EnemyCount){
            int sum = 0;
            for(int i = 0; i < enemyRatios.Length; i++){
                sum += enemyRatios[i];
            }
            for(int j = 0; j < currentSpawnSize; j++){
                int rand = Random.Range(0, sum);
                int counter = sum - enemyRatios[0];
                for(int i = 0; i < availableEnemies.Length; i++){
                    if(rand >= counter){
                        currentArea.GetComponent<WaveArea>().spawnEnemy(availableEnemies[i],enemyHealthBuffTotal, enemyDamageBuffTotal,this.gameObject);
                        spawnCount++;
                        break;
                    }else{
                        if(i + 1 < enemyRatios.Length){
                            counter -= enemyRatios[i + 1];
                        }
                        
                    }
                }         
            }
              
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
    public void SkipWave(){
        skipWave =true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            skipWave = true;
        }
    }
}
