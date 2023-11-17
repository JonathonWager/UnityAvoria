using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class uiUpdater : MonoBehaviour
{
    public GameObject player;
    public Image panel;
    int hp;
    bool dash;

    Weapon curWeapon;
     public float newAlpha;
    public TMP_Text hpUI,sprUI,invUI;
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


        hpUI.text = "HP: " + hp;
        //sprUI.text = "Sprint: ";
        invUI.text = curWeapon.getName(); 
    }
}
