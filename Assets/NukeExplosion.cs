using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeExplosion : MonoBehaviour
{
    // Start is called before the first frame update
     public float expansionTime = 2f;
    public float expansionUpdateFreq = 0.1f;
    public float expansionIncriment = 0.3f;

     Vector3 scaleChange;
    float elapsed = 0f;

    public void  OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "enemy"){
            other.gameObject.GetComponent<enemyStats>().takeDamage(1000,new Vector2(0.0f, 0.0f),0f);        
        }
    }
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
