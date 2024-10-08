using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public string bossName;

    public GameObject UI,waveManger;
    public GameObject boss,towerArray;
    public GameObject barrier;
    bool isActive = false;
    int interactionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        waveManger = GameObject.FindGameObjectWithTag("WaveManager");
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            interactionCount++;
            if(interactionCount == 1){
                isActive = true;
                waveManger.GetComponent<WaveManager>().PauseSpawning();
                 barrier.GetComponent<BossBlockade>().ResetBarrier();
                UI.GetComponent<uiUpdater>().DisableWaveAccept();
            }else if(interactionCount == 2){
                boss.SetActive(true);
                towerArray.SetActive(true);
            }
           
            
            
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive){
            int health = boss.GetComponent<bossStats>().hp;
            int maxHealth = boss.GetComponent<bossStats>().maxHPDontSet;
            UI.GetComponent<uiUpdater>().SetBossUI(boss.gameObject.name, health, maxHealth);
        }
    }
}
