using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class uiUpdater : MonoBehaviour
{
    public GameObject player;
    public Image panel;
    public Image qAbilityPanel, eAbilityPanel;
    int hp;
    Weapon curWeapon;

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

    public void showShop(List<string> shopItems)
    {
        shopUI.enabled = true;
        shopUI.text = "Big Shop";
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopUI.text = shopUI.text + "\n" + shopItems[i] + " Press " + (i + 1);
        }
    }

    public void disableShop()
    {
        shopUI.enabled = false;
        shopUI.text = "";
    }

    public void UpdateAbilityUI(bool isQOnCooldown, bool isEOnCooldown)
    {
        if (qAbilityPanel != null)
        {
            qAbilityPanel.color = isQOnCooldown ? new Color32(233, 67, 67, 100) : Color.white;
        }

        if (eAbilityPanel != null)
        {
            eAbilityPanel.color = isEOnCooldown ? new Color32(233, 67, 67, 100) : Color.white;
        }
    }

    void updatehpUI()
    {
        characterStats cStats = player.GetComponent<characterStats>();
        hp = cStats.getHp();
        hpUI.text = "HP: " + hp;
    }

    void updateinvUI()
    {
        InventoryV3 iStats = player.transform.GetChild(1).gameObject.GetComponent<InventoryV3>();
        curWeapon = iStats.getCurrentWeapon();
        invUI.text = curWeapon.weaponName;
    }

    void updateGoldUI()
    {
        characterStats cStats = player.GetComponent<characterStats>();
        goldUI.text = "Gold: " + cStats.gold;
    }

    void Update()
    {
        updatehpUI();
        updateinvUI();
        updateGoldUI();
    }
}
