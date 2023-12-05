using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bossUi : MonoBehaviour
{
    GameObject beegBoss = null;
    public TMP_Text myTextMeshProObject;
     public TMP_Text hpUI,invUI, qUI, eUI, potionUI;

     int maxhp;
    // Start is called before the first frame update
    void Start()
    {
        myTextMeshProObject  = this.transform.GetComponent<TextMeshProUGUI>();
        if(GameObject.Find("beegBossV2") != null){
            beegBoss = GameObject.Find("beegBossV2");
            bossStats bStats = beegBoss.GetComponent<bossStats>();
            maxhp  =  bStats.hp;
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        bossStats bStats = beegBoss.GetComponent<bossStats>();
        myTextMeshProObject.text = "beegBoss: "+ bStats.hp + "/" + maxhp ;
    }
}
