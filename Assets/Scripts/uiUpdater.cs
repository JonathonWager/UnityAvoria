using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiUpdater : MonoBehaviour
{
    public GameObject player;
    int hp;
    int sprint;

    weapon curWeapon;

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


        movement mStats = player.GetComponent<movement>();
        sprint = mStats.getSprint();

        inventory iStats = player.transform.GetChild(0).gameObject.GetComponent<inventory>();
        curWeapon = iStats.getWeapon();


        hpUI.text = "HP: " + hp;
        sprUI.text = "Sprint: " + sprint;
        invUI.text = curWeapon.getName(); 
    }
}
