using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AbilitySystem;
public class uiUpdater : MonoBehaviour
{
    public GameObject player;
    public Image panel;
    public Image qAbilityPanel, eAbilityPanel;
    public Text hpUI,goldUI;

    public TMP_Text   qUI, eUI, potionUI, shopUI;
    Text[] invUI;
    Image[] invIcons;
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
            if (child.name == "qUI")
            {
                qUI = child.GetComponent<TextMeshProUGUI>();
            }
            if (child.name == "eUI")
            {
                eUI = child.GetComponent<TextMeshProUGUI>();
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
        }
    }

    void Update()
    {
        updatehpUI();
        updatesprintUI();
        updateinvUI();
        //updateqeUI();
        //updatepotionUI();
        updateGoldUI();
    }

    public void UpdateAbilityUI(bool isQOnCooldown, bool isEOnCooldown)
    {
        qAbilityPanel.color = isQOnCooldown ? new Color32(233, 67, 67, 100) : Color.white;
        eAbilityPanel.color = isEOnCooldown ? new Color32(233, 67, 67, 100) : Color.white;
    }

    void updatehpUI()
    {
        characterStats cStats = player.GetComponent<characterStats>();
        hpUI.text = "100 / " + cStats.getHp();
    }

    void updatesprintUI()
    {
        playerMovement mStats = player.GetComponentInChildren<playerMovement>();
        bool dash = mStats.getDashStatus();

        panel.color = dash ?   new Color32(84, 160, 255, 168) : Color.white;
    }

    void updateinvUI()
    {
         InventoryV3 iStats = player.GetComponentInChildren<InventoryV3>();
         Weapon CurrentWeapon = iStats.getCurrentWeapon();
         for(int i = 0; i < 2; i++){
            invUI[i].text = iStats.getWeapons()[i].weaponName;
            if(CurrentWeapon.weaponName == iStats.getWeapons()[i].weaponName){
                invIcons[i+1].color = Color.white;
            }else{
                invIcons[i+1].color = new Color32(72, 219, 251, 255);
            }
         }
         
        
       
       
 
    }

    void updateqeUI()
    {
        AbilityManager abilityManager = player.GetComponentInChildren<AbilityManager>();

        if (abilityManager != null)
        {
            qUI.text = abilityManager.currentQ != null ? abilityManager.currentQ.abilityName : "None";
            eUI.text = abilityManager.currentE != null ? abilityManager.currentE.abilityName : "None";
            UpdateAbilityUI(abilityManager.IsQOnCooldown, abilityManager.IsEOnCooldown);
        }
    }

    void updatepotionUI()
    {
        potionUI.text = "Potions:";
        InventoryV3 iStats = player.GetComponentInChildren<InventoryV3>();
        Dictionary<string, int> potionDictionary = iStats.getpotionDictionary();
        foreach (var kvp in potionDictionary)
        {
            if (kvp.Value > 0)
            {
                potionUI.text += "\n" + kvp.Key + ": " + kvp.Value;
            }
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
