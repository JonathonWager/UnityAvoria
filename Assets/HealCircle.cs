using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public float size;
    public int healAmount;
    public float healInterval;

     GameObject healEffect;
    bool isHealing = false;
    GameObject player;
    characterStats cStats;

    float elapsed;
    public void SetStats(float radius, int heal, float interval ,float duration){
        size = radius;
        healAmount = heal;
        healInterval = interval;
        elapsed = healInterval;
        gameObject.transform.localScale = new Vector3(radius, radius, 1f);

        Destroy(this.gameObject, duration);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("character")){
            if(!isHealing){
                player = other.gameObject;
                cStats = player.GetComponent<characterStats>();
                isHealing = true;
                healEffect.SetActive(true);
            }
            
        }
    }
     void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("character")){
            if(isHealing){
                isHealing = false;
                healEffect.SetActive(false);
            }
        }
    }
    void Start()
    {

        foreach(Transform t in transform){
            if(t.name == "HealEffect"){
                healEffect = t.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(isHealing){
            if(elapsed >= healInterval){
                if(cStats.hp + healAmount > cStats.basehp){
                    if(cStats.hp < cStats.basehp){
                        cStats.hp = cStats.basehp;
                        elapsed= 0f;
                    }
                }else{
                    cStats.hp += healAmount;
                    elapsed = 0f;
                }              
            }
            healEffect.transform.position = player.transform.position;
        }
    }
}
