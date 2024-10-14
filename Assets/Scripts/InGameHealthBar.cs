using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameHealthBar : MonoBehaviour
{
    public GameObject enemy;
    Image healthBar;
    Text healthBarText;
    void Start()
    {
        foreach(Transform child in transform){
                if(child.gameObject.name == "Scaler"){
                    healthBar = child.gameObject.GetComponent<Image>();
                }
                if(child.gameObject.name == "Health"){
                    healthBarText = child.gameObject.GetComponent<Text>();
                }
             }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.GetComponent<enemyStats>().hp > 0){
             healthBarText.text = enemy.GetComponent<enemyStats>().hp + "/" + enemy.GetComponent<enemyStats>().maxHPDontSet;
            healthBar.fillAmount = Mathf.Clamp01((float)enemy.GetComponent<enemyStats>().hp / (float)enemy.GetComponent<enemyStats>().maxHPDontSet);

        }else{
            Destroy(this.gameObject);
        }
    }
}
