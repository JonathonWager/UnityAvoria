using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public string bossName;

    public GameObject UI,waveManger;
    public GameObject boss,towerArray;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        waveManger = GameObject.FindGameObjectWithTag("WaveManager");
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            waveManger.GetComponent<WaveManager>().PauseSpawning();
            boss.SetActive(true);
            towerArray.SetActive(true);
            isActive = true;
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
