using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffShop : MonoBehaviour
{
    public GameObject UI;
    GameObject player;

    public int hpBuff = 0;
    public float speedBuff = 0;
    public int dmgBuff = 0;
    public float rangeBuff = 0;

    public int hpBuffIncrease = 10;
    public float speedBuffIncrease = 1;
    public int dmgBuffIncrease = 5;
    public float rangeBuffIncrease = 0.25f;

    public int hpBuffCost = 50;
    public int speedBuffCost = 50;
    public int dmgBuffCost = 50;
    public int rangeBuffCost = 50;

    public int hpLevel = 1;
    public int speedLevel = 1;
    public int rangeLevel = 1;
    public int dmgLevel = 1;

    public bool shopActive = false;
    public GameObject buffStats;  // Drag and drop BuffStats in the editor or find it in code

    private GameObject statEntryDmg;
    private GameObject statEntrySpeed;
    private GameObject statEntryRange;
    private GameObject statEntryHp;

    void Start()
    {
        // Find the children of buffStats by name
        statEntryDmg = buffStats.transform.Find("StatEntryDmg").gameObject;
        statEntrySpeed = buffStats.transform.Find("StatEntrySpeed").gameObject;
        statEntryRange = buffStats.transform.Find("StatEntryRange").gameObject;
        statEntryHp = buffStats.transform.Find("StatEntryHp").gameObject;
    }
    // Start is called before the first frame update
     public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "character")
        {  
            player = other.gameObject; 
             SetUI();
            shopActive = true; 

            //showBuffPrompt();
        }
    }
    void SetUI(){
        buffStats.SetActive(true);
        foreach(Transform child in statEntryDmg.transform){
            if(child.name == "Cost"){
                child.GetComponent<Text>().text = dmgBuffCost.ToString();
            }
            if(child.name == "Increase"){
                child.GetComponent<Text>().text = dmgBuffIncrease.ToString();
            }
            if(child.name == "UpgradedAmount"){
                child.GetComponent<Text>().text = (dmgBuff + dmgBuffIncrease).ToString();
            }
            if(child.name == "CurrentLevel"){
                child.GetComponent<Text>().text  = "Level " + dmgLevel;
            }
            if(child.name == "CurrentAmount"){
                child.GetComponent<Text>().text = dmgBuff.ToString();
            }
        }
        foreach(Transform child in statEntryHp.transform){
            if(child.name == "Cost"){
                child.GetComponent<Text>().text = hpBuffCost.ToString();
            }
            if(child.name == "Increase"){
                child.GetComponent<Text>().text = hpBuffIncrease.ToString();
            }
            if(child.name == "UpgradedAmount"){
                child.GetComponent<Text>().text = (hpBuff + hpBuffIncrease).ToString();
            }
            if(child.name == "CurrentLevel"){
                child.GetComponent<Text>().text  = "Level " + hpLevel;
            }
            if(child.name == "CurrentAmount"){
                child.GetComponent<Text>().text = hpBuff.ToString();
            }
        }
        foreach(Transform child in statEntrySpeed.transform){
            if(child.name == "Cost"){
                child.GetComponent<Text>().text = speedBuffCost.ToString();
            }
            if(child.name == "Increase"){
                child.GetComponent<Text>().text = speedBuffIncrease.ToString();
            }
            if(child.name == "UpgradedAmount"){
                child.GetComponent<Text>().text = (speedBuff + speedBuffIncrease).ToString();
            }
            if(child.name == "CurrentLevel"){
                child.GetComponent<Text>().text  = "Level " + speedLevel;
            }
            if(child.name == "CurrentAmount"){
                child.GetComponent<Text>().text = speedBuff.ToString();
            }
        }
        foreach(Transform child in statEntryRange.transform){
            if(child.name == "Cost"){
                child.GetComponent<Text>().text = rangeBuffCost.ToString();
            }
            if(child.name == "Increase"){
                child.GetComponent<Text>().text = rangeBuffIncrease.ToString();
            }
            if(child.name == "UpgradedAmount"){
                child.GetComponent<Text>().text = (rangeBuff + rangeBuffIncrease).ToString();
            }
            if(child.name == "CurrentLevel"){
                child.GetComponent<Text>().text  = "Level " + rangeLevel;
            }
            if(child.name == "CurrentAmount"){
                child.GetComponent<Text>().text = rangeBuff.ToString();
            }
        }

    }
      public void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.tag == "character")
        {  
            buffStats.SetActive(false);
        }
    }

    public void DamageBuy(){
        if(player.GetComponent<characterStats>().gold > dmgBuffCost){
            dmgBuff += dmgBuffIncrease;
            player.GetComponent<characterStats>().dmgBuff += dmgBuffIncrease;
            player.GetComponent<characterStats>().gold -= dmgBuffCost;
            dmgBuffCost = dmgBuffCost * 2;
            dmgLevel += 1;



            SetUI();
        }
        
    }
    public void RangeBuy(){
        if(player.GetComponent<characterStats>().gold > rangeBuffCost){
            rangeBuff += rangeBuffIncrease;
            player.GetComponent<characterStats>().rangeBuff += rangeBuffIncrease;
             player.GetComponent<characterStats>().gold -= rangeBuffCost;
            rangeBuffCost = rangeBuffCost * 2;
            rangeLevel += 1;


           
            SetUI();
        }
    }
    public void HpBuy(){
        if(player.GetComponent<characterStats>().gold > hpBuffCost){
            hpBuff += hpBuffIncrease;
            player.GetComponent<characterStats>().basehp += hpBuffIncrease;
            player.GetComponent<characterStats>().gold -= hpBuffCost;
            hpBuffIncrease = hpBuffIncrease * 2;
            hpBuffCost = hpBuffCost * 2;
            hpLevel += 1;

            
            
            SetUI();
        }
    }
    public void SpeedBuy(){
        if(player.GetComponent<characterStats>().gold > speedBuffCost){
            speedBuff += speedBuffIncrease;
            player.GetComponent<characterStats>().movementBuff += speedBuffIncrease;
            player.GetComponent<characterStats>().gold -= speedBuffCost;
            speedBuffCost = speedBuffCost * 2;
            speedLevel += 1;


             
            SetUI();
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
