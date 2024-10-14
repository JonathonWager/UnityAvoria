using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public bool magnetActive = false;
    public float magnetDistance = 10f;
    public float magnetDuration = 60f;

    public float vineRange = 5f;
    public float vineSlowAmount = 2f;
    public float vineDuration = 5f;

    public bool slowingVinesActive = false;

    float elapsed = 0f;
     float elapsedVines = 0f;



    // Update is called once per frame

    void FindDrops(){

        GameObject[] allDrops = GameObject.FindGameObjectsWithTag("Drop");
        float rangeSqr = magnetDistance * magnetDistance;
        foreach(GameObject drop in allDrops){
            float distance =(drop.transform.position - this.transform.position).sqrMagnitude;
            if (distance <= rangeSqr){
                drop.GetComponent<MagnetEffect>().MagnetStart(this.gameObject,distance,rangeSqr );
            }
        }


    }
       public void MagnetEnable(){
        magnetActive = true;
        foreach (Transform child in transform)
        {
            if(child.gameObject.name == "MagnetEffect"){
                child.gameObject.SetActive(true);
            }   

        }
        elapsed = 0f;
    }

    void SlowingVines(){
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("enemy");
        float rangeSqr = vineRange * vineRange;
        foreach(GameObject enemy in allEnemies){
            float distance =(enemy.transform.position - this.transform.position).sqrMagnitude;
            if (distance <= rangeSqr){
                enemy.GetComponent<enemyStats>().setVineSlowing(vineSlowAmount);
            }
        }
    }
 
    public void VinesEnable(float slow, float duration,float range){
        if(slowingVinesActive){
            if(slow > vineSlowAmount){
                vineSlowAmount = slow;
                vineDuration = duration;
                vineRange = range;
            }
        }else{
            vineRange = range;
            vineSlowAmount = slow;
            vineDuration = duration;
        }
         foreach (Transform child in transform)
        {
            if(child.gameObject.name == "VineEffect"){
                child.gameObject.SetActive(true);
                child.GetComponent<VineEffectSizer>().lightSize = vineRange;
            }   

        }
        slowingVinesActive = true;
        elapsedVines = 0f;
        
    }
    void Update()
    {
        elapsed += Time.deltaTime;
        elapsedVines += Time.deltaTime;
        if(magnetActive){
             FindDrops();
             if(elapsed >= magnetDuration){
                magnetActive = false;
                 foreach (Transform child in transform)
                {
                    if(child.gameObject.name == "MagnetEffect"){
                        child.gameObject.SetActive(false);
                    }
                }
             }
        }

        if(slowingVinesActive){
            SlowingVines();
            if(elapsedVines >= vineDuration){
                slowingVinesActive = false;
                 foreach (Transform child in transform)
                {
                    if(child.gameObject.name == "VineEffect"){
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
