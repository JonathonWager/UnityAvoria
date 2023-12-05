using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossExplosion : MonoBehaviour
{
    public float expansionTime = 2f;
    public float expansionUpdateFreq = 0.1f;
    public float expansionIncriment = 0.3f;

    Vector3 scaleChange;
    float elapsed = 0f;
    public int explosionDmg = 25;
    public float explosionDmgInterval = 0.5f;
    bool startedDmg = false;
    GameObject playerTarget;
    private void dealDmg(){
        characterStats cStats = playerTarget.GetComponent<characterStats>();  
        cStats.takeDamage(explosionDmg);
    }
    public void  OnTriggerEnter2D(Collider2D other){
         if(other.gameObject.tag == "character"){
            if(!startedDmg){
                InvokeRepeating("dealDmg", 0f, explosionDmgInterval);
                playerTarget = other.gameObject;
            }
         }
    }
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(expansionIncriment, expansionIncriment, 0f);
        InvokeRepeating("Expand",0f, expansionUpdateFreq);
    }
    void Expand(){
        transform.localScale += scaleChange;
    }
    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= expansionTime){
            Destroy(this.gameObject);
        }
    }
}
