using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AbilitySystem;
using WeaponsSystem;
public class uiUpdater : MonoBehaviour
{
    public GameObject player;
    public Image panel;
    public GameObject ResetWeapons,ResetAbilits;
    public Transform WaveStartPanel, bossPanel;
    public Image qAbilityPanel, eAbilityPanel;
    public Text hpUI,goldUI, waveUI, weaponLevelUp, abilityLevelUp;

    public TMP_Text   qUI, eUI, potionUI, shopUI;
    public Text[] invUI, abilityUi,waveCountdown, bossStats;
    Image[] invIcons,abilityIcons;
    bool Switch = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");

        foreach (Transform child in transform)
        {
            if (child.name == "HP")
            {
                hpUI = child.GetComponentInChildren<Text>();
            }
            if (child.name == "Weapons")
            {
                invUI = child.GetComponentsInChildren<Text>();
                invIcons = child.GetComponentsInChildren<Image>();
            }
            if (child.name == "Abilitys")
            {
                abilityUi = child.GetComponentsInChildren<Text>();
                abilityIcons = child.GetComponentsInChildren<Image>();
            }
            if (child.name == "potionUI")
            {
                potionUI = child.GetComponent<TextMeshProUGUI>();
            }
            if (child.name == "shopUI")
            {
                shopUI = child.GetComponent<TextMeshProUGUI>();
            }
            if (child.name == "Gold")
            {
                goldUI = child.GetComponentInChildren<Text>();
            }
            if (child.name == "WaveCounter")
            {
                waveUI = child.GetComponentInChildren<Text>();
            }
            if (child.name == "BossPanel")
            {
                bossPanel = child;
                bossStats = child.GetComponentsInChildren<Text>();
            }
            if (child.name == "WeaponsLevel")
            {
                ResetWeapons = child.gameObject;
                weaponLevelUp = child.GetComponentInChildren<Text>();
            }
            if (child.name == "AbilityLevel")
            {
                ResetAbilits = child.gameObject;
                abilityLevelUp = child.GetComponentInChildren<Text>();
            }
            if (child.name == "NextWave")
            {
                waveCountdown = child.GetComponentsInChildren<Text>();
                WaveStartPanel = child;
            }
        }
    }

    void Update()
    {
        updatehpUI();
        updatesprintUI();
        updateinvUI();
        updateAbilities();
        //updatepotionUI();
        updateGoldUI();
    }
    public void nextWaveUI(int second, int wave){
        if(!Switch){
            Switch = true;
            foreach (Transform child in transform)
            {
                if (child.name == "NextWave")
                {
                    waveCountdown = child.GetComponentsInChildren<Text>();
                    WaveStartPanel = child;

                }
            }

       }
        
        waveCountdown[0].text = "Wave "+ wave +" Starting in: " + second;
    }
    public void SetBossUI(string bossName, int bossHealth, int bossMaxHealth){
        bossPanel.gameObject.SetActive(true);
        bossStats[1].text = bossName;
        bossStats[0].text = bossHealth +"/"+bossMaxHealth;
    }
    public void WeaponsLevelUp(string name, int level){
        ResetWeapons.gameObject.SetActive(true);
        weaponLevelUp.text = "Level up \n" + name + " " + level;
        Invoke("DisableWeaponsLevel", 10f);
    }
    void DisableWeaponsLevel(){
        ResetWeapons.gameObject.SetActive(false);
    }
    public void AbilityLevelUp(string name, int level){
        ResetAbilits.gameObject.SetActive(true);
        abilityLevelUp.text = "Level up \n" + name + " " + level;
        Invoke("DisableAbilitysLevel", 10f);
    }
    void DisableAbilitysLevel(){
        ResetAbilits.gameObject.SetActive(false);
    }
    public void GameOver(int score, int kills, int damage, int gold, int wave){
        foreach (Transform child in transform)
        {
            // If the child's name matches the specified name, keep it active
            if (child.gameObject.name == "GameOver")
            {
                child.gameObject.SetActive(true);
                foreach(Transform subChild in child){
                    if(subChild.gameObject.name == "Score"){
                        subChild.gameObject.GetComponent<Text>().text = "Score: " + score;
                    }
                    if(subChild.gameObject.name == "Kills"){
                        subChild.gameObject.GetComponent<Text>().text = "Kills: " + kills;
                    }
                    if(subChild.gameObject.name == "Gold"){
                        subChild.gameObject.GetComponent<Text>().text = "Gold: " + gold;
                    }
                    if(subChild.gameObject.name == "Damage"){
                        subChild.gameObject.GetComponent<Text>().text = "Damage: " + damage;
                    }
                    if(subChild.gameObject.name == "Wave"){
                        subChild.gameObject.GetComponent<Text>().text = "Wave: " + wave;
                    }
                }
            }
            else
            {
                // Otherwise, disable the child object
                child.gameObject.SetActive(false);
            }
        }
    }
    public void EnableWaveAccept(){
        WaveStartPanel.gameObject.SetActive(true);
    }
      public void DisableWaveAccept(){
        WaveStartPanel.gameObject.SetActive(false);
    }
    public void UpdateAbilityUI(bool isQOnCooldown, bool isEOnCooldown)
    {
        qAbilityPanel.color = isQOnCooldown ? new Color32(233, 67, 67, 100) : Color.white;
        eAbilityPanel.color = isEOnCooldown ? new Color32(233, 67, 67, 100) : Color.white;
    }
    public void updateWave(int Wave){
        waveUI.text = Wave.ToString();
    }
    void updatehpUI()
    {
        characterStats cStats = player.GetComponent<characterStats>();
        hpUI.text = cStats.getHp()+ " / " +  cStats.basehp;
    }

    void updatesprintUI()
    {
        playerMovement mStats = player.GetComponentInChildren<playerMovement>();
        bool dash = mStats.getDashStatus();

        panel.color = dash ?   new Color32(84, 160, 255, 168) : Color.white;
    }

    void updateinvUI()
    {
     
        InventoryV4 iStats = player.GetComponentInChildren<InventoryV4>();
        WeaponBase CurrentWeapon = iStats.currentWeapon;
        WeaponBase[] Weapons = iStats.InvWeapons;
         for(int i = 0; i < 2; i++){
            invUI[i].text = Weapons[i].name;
            if(CurrentWeapon.name == Weapons[i].name){
                invIcons[i+1].color = Color.white;
            }else{
                invIcons[i+1].color = new Color32(72, 219, 251, 255);
            }
            
         }
         invIcons[1].sprite = Weapons[0].icon;
         invIcons[2].sprite = Weapons[1].icon;

         invUI[2].text = Weapons[0].level.ToString();
         invUI[3].text = Weapons[1].level.ToString();
        
        if(CurrentWeapon is ChargedRanged chargedRangedWeapon){
            invIcons[3].fillAmount = Mathf.Clamp01(chargedRangedWeapon.chargeTime/ chargedRangedWeapon.maxCharge);
        }
       
 
    }

    void updateAbilities()
    {
        AbilityManager abilityManager = player.GetComponentInChildren<AbilityManager>();

        if (abilityManager != null){
            abilityUi[0].text =  abilityManager.currentQ != null ? abilityManager.currentQ.abilityName : "None";
            abilityUi[1].text =  abilityManager.currentE != null ? abilityManager.currentE.abilityName : "None";
            abilityUi[2].text =  abilityManager.currentE != null ? abilityManager.currentQ.level.ToString() : "None";
            abilityUi[3].text =  abilityManager.currentE != null ? abilityManager.currentE.level.ToString() : "None";
            
            abilityIcons[1].sprite = abilityManager.currentQ != null ? abilityManager.currentQ.icon : null;
                        
            abilityIcons[3].sprite = abilityManager.currentQ != null ? abilityManager.currentE.icon : null;
            //UpdateAbilityUI(abilityManager.IsQOnCooldown, abilityManager.IsEOnCooldown);
        }
    }



    void updateGoldUI()
    {
        characterStats cStats = player.GetComponent<characterStats>();
        goldUI.text = cStats.gold.ToString();
    }

    public void showShop(List<string> shopItems)
    {
        shopUI.enabled = true;
        shopUI.text = "Big Shop";
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopUI.text += "\n" + shopItems[i] + " Press " + (i + 1);
        }
    }

    public void disableShop()
    {
        shopUI.enabled = false;
        shopUI.text = "";
    }
}
