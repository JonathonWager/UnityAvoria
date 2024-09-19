using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe : MonoBehaviour
{
    private int damage = 20;
    private float range = 3f;
    private float spinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spinAttcker sStats = GetComponentInParent<spinAttcker>();
        damage = sStats.getDamage();
        range = sStats.getRange();
        spinSpeed = sStats.spinSpeed;
    }
    public void  OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "character"){
            characterStats cStats = other.gameObject.GetComponent<characterStats>();
            cStats.takeDamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
