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

    public TMP_Text hpUI, invUI, qUI, eUI, potionUI, shopUI, goldUI;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");

        foreach (Transform child in transform)
        {
            if (child.name == "hpUI")
            {
                hpUI = child.GetComponent<TextMeshProUGUI>();
            }
            if (child.name == "invUI")
            {
                invUI = child.GetComponent<TextMeshProUGUI>();
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
            if (child.name == "goldUI")
            {
                goldUI = child.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    void Update()
    {
        updatehpUI();
        updatesprintUI();
        updateinvUI();
        updateqeUI();
        updatepotionUI();
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
        hpUI.text = "HP: " + cStats.getHp();
    }

    void updatesprintUI()
    {
        playerMovement mStats = player.GetComponent<playerMovement>();
        bool dash = mStats.getDashStatus();
        panel.color = dash ? Color.white : new Color32(233, 67, 67, 100);
    }

    void updateinvUI()
    {
        InventoryV3 iStats = player.GetComponentInChildren<InventoryV3>();
        Weapon curWeapon = iStats.getCurrentWeapon();
        invUI.text = curWeapon.weaponName;
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
        goldUI.text = "Gold: " + cStats.gold;
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
