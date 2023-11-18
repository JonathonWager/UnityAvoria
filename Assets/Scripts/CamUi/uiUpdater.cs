using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class uiUpdater : MonoBehaviour
{
    public GameObject player;
    public Image panel;
    public Image qAbilityPanel,eAbilityPanel;
    int hp;
    bool dash;
    bool qAbility,eAbility;
    Weapon curWeapon;

    public TMP_Text hpUI,sprUI,invUI, qUI, eUI;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform){
            if(child.name == "hpUI"){
                hpUI = child.GetComponent<TextMeshProUGUI>();
            }
            if(child.name == "sprintUI"){
                sprUI = child.GetComponent<TextMeshProUGUI>();
            }
            if(child.name == "invUI"){
                invUI = child.GetComponent<TextMeshProUGUI>();
            }
        }
    }
            

    // Update is called once per frame
    void Update()
    {
        
        characterStats cStats = player.GetComponent<characterStats>();
        hp = cStats.getHp();


        playerMovement mStats = player.GetComponent<playerMovement>();
        dash = mStats.getDashStatus();
        Image img = panel.GetComponent<Image>();
        if(dash){
            img.color = UnityEngine.Color.white;
        }else{
            img.color =   new Color32( 233 , 67 , 67 , 100);
        }
        
        InventoryV12 iStats = player.transform.GetChild(1).gameObject.GetComponent<InventoryV12>();
        curWeapon = iStats.getCurrentWeapon();

        abilityDirector aStats = player.transform.GetChild(2).gameObject.GetComponent<abilityDirector>();
        qAbility = aStats.getQ();
        Image img2 = qAbilityPanel.GetComponent<Image>();
         if(qAbility){
            img2.color = UnityEngine.Color.white;
        }else{
            img2.color =   new Color32( 233 , 67 , 67 , 100);
        }
        qUI.text = aStats.getCurrentQ().getName();

        Image img3 = eAbilityPanel.GetComponent<Image>();
        eAbility = aStats.getE();
        if(eAbility){
            img3.color = UnityEngine.Color.white;
        }else{
            img3.color =   new Color32( 233 , 67 , 67 , 100);
        }
        eUI.text = aStats.getCurrentE().getName();

        hpUI.text = "HP: " + hp;
        //sprUI.text = "Sprint: ";
        invUI.text = curWeapon.getName(); 
    }
}
