using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffShop : MonoBehaviour
{
    public GameObject UI;
    GameObject player;

    public int healthRegenBuff = 1;
    public float waitTimerBuff = 15f;
    public int hpBuff = 0;
    public float speedBuff = 0;
    public int dmgBuff = 0;
    public float rangeBuff = 0;

    public float waitTimerIncrease = 15f;
    public int hpBuffIncrease = 10;
    public float speedBuffIncrease = 1;
    public int dmgBuffIncrease = 5;
    public float rangeBuffIncrease = 0.25f;
    public int healthRegenIncrease = 2;

    public int waveTimerCost = 50;
    public int hpBuffCost = 50;
    public int speedBuffCost = 50;
    public int dmgBuffCost = 50;
    public int rangeBuffCost = 50;
    public int healthRegenCost = 50;

    public int waveTimerLevel =1;
    public int hpLevel = 1;
    public int speedLevel = 1;
    public int rangeLevel = 1;
    public int dmgLevel = 1;
    public int healthRegenLevel = 1;


    public GameObject buffStats;  // Drag and drop BuffStats in the editor or find it in code

    private GameObject statEntryTimer;
    private GameObject statEntrySpeed;
    private GameObject statEntryRegen;
    private GameObject statEntryHp;
 private bool isPrompt = false;
 private bool isActive = false;
    void Start()
    {
         UI = GameObject.FindGameObjectWithTag("UI");
        // Find the children of buffStats by name
        statEntryTimer = buffStats.transform.Find("StatEntryTimer").gameObject;
        statEntrySpeed = buffStats.transform.Find("StatEntrySpeed").gameObject;
        statEntryRegen = buffStats.transform.Find("StatEntryRegen").gameObject;
        statEntryHp = buffStats.transform.Find("StatEntryHp").gameObject;
    }
    // Start is called before the first frame update
     public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "character")
        {  
            player = other.gameObject; 
            showShopPrompt();


            //showBuffPrompt();
        }
    }
    void SetUI(){
        buffStats.SetActive(true);
        foreach(Transform child in statEntryTimer.transform){
            if(child.name == "Cost"){
                child.GetComponent<Text>().text = waveTimerCost.ToString();
            }
            if(child.name == "Increase"){
                child.GetComponent<Text>().text = waitTimerIncrease.ToString();
            }
            if(child.name == "UpgradedAmount"){
                child.GetComponent<Text>().text = (waitTimerBuff + waitTimerIncrease).ToString();
            }
            if(child.name == "CurrentLevel"){
                child.GetComponent<Text>().text  = "Level " + waveTimerLevel;
            }
            if(child.name == "CurrentAmount"){
                child.GetComponent<Text>().text = waitTimerBuff.ToString();
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
                child.GetComponent<Text>().text = (player.GetComponent<characterStats>().basehp  + hpBuffIncrease).ToString();
            }
            if(child.name == "CurrentLevel"){
                child.GetComponent<Text>().text  = "Level " + hpLevel;
            }
            if(child.name == "CurrentAmount"){
                child.GetComponent<Text>().text = (player.GetComponent<characterStats>().basehp).ToString();
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
        foreach(Transform child in statEntryRegen.transform){
            if(child.name == "Cost"){
                child.GetComponent<Text>().text = healthRegenCost.ToString();
            }
            if(child.name == "Increase"){
                child.GetComponent<Text>().text = healthRegenIncrease.ToString();
            }
            if(child.name == "UpgradedAmount"){
                child.GetComponent<Text>().text = (healthRegenBuff + healthRegenIncrease).ToString();
            }
            if(child.name == "CurrentLevel"){
                child.GetComponent<Text>().text  = "Level " + healthRegenLevel;
            }
            if(child.name == "CurrentAmount"){
                child.GetComponent<Text>().text = healthRegenBuff.ToString();
            }
        }

    }
      public void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.tag == "character")
        {  
            buffStats.SetActive(false);
            ToggleChildByName(UI, "ShopBlur", false);
            isActive = false;
            isPrompt = false;
        }
    }
    public void RegenBuy(){
        if(player.GetComponent<characterStats>().gold >= healthRegenCost){
             player.GetComponent<characterStats>().gold -= healthRegenCost;
             player.GetComponent<characterStats>().regenAmount += healthRegenIncrease;
             healthRegenBuff += healthRegenIncrease;
             healthRegenCost = healthRegenCost * 2;
             healthRegenLevel +=1;
             SetUI();
        }
    }
    public void WaitTimerBuy(){
        if(player.GetComponent<characterStats>().gold >= waveTimerCost){
            GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>().roundTransitionTime += waitTimerIncrease;
             player.GetComponent<characterStats>().gold -= waveTimerCost;
            waitTimerBuff += waitTimerIncrease;
            waveTimerCost = (int)(waveTimerCost * 1.5);
            waveTimerLevel += 1;
              SetUI();
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
        if(player.GetComponent<characterStats>().gold >= hpBuffCost){
            hpBuff += hpBuffIncrease;
            player.GetComponent<characterStats>().basehp += hpBuffIncrease;
            if(player.GetComponent<characterStats>().hp + hpBuffIncrease > player.GetComponent<characterStats>().basehp ){
                 player.GetComponent<characterStats>().hp =  player.GetComponent<characterStats>().basehp;
            }else{
                player.GetComponent<characterStats>().hp+= hpBuffIncrease;
            }
           
            player.GetComponent<characterStats>().gold -= hpBuffCost;
            hpBuffIncrease = (int)(hpBuffIncrease * 1.75);
            hpBuffCost = (int)(hpBuffCost * 1.75);
            hpLevel += 1;

            
            
            SetUI();
        }
    }
    public void SpeedBuy(){
        if(player.GetComponent<characterStats>().gold >= speedBuffCost){
            speedBuff += speedBuffIncrease;
            player.GetComponent<characterStats>().movementBuff += speedBuffIncrease;
            player.GetComponent<characterStats>().gold -= speedBuffCost;
            speedBuffCost = (int)(speedBuffCost * 1.75);
            speedLevel += 1;


             
            SetUI();
        }
    }

     void showShopPrompt(){
        ToggleChildByName(UI, "ShopPrompt", true);
        isPrompt = true;
    }
        public void ToggleChildByName(GameObject parent, string childName, bool isActive)
    {
        Transform childTransform = parent.transform.Find(childName);
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning($"Child with name '{childName}' not found.");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive){
            if (Input.GetKeyDown(KeyCode.F)){
                buffStats.SetActive(false);
                ToggleChildByName(UI, "ShopBlur", false);
                isActive = false;
                //isPrompt = true;
                //showShopPrompt();
            }
        }
         if(isPrompt){
            if (Input.GetKeyDown(KeyCode.F)){
                ToggleChildByName(UI, "ShopPrompt", false);
                ToggleChildByName(UI, "ShopBlur", true);
                SetUI();
                isPrompt = false;
                isActive = true;
            }
        }
    }
}
