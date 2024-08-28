using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrimFireV2 : MonoBehaviour
{

    public float burnInterval = 3f;
    public float destroyTime = 5f;
    public int burnDmg = 10;

    private GameObject playerTarget;
    public float waitForBurn = 2f;

    private void dealDmg(){
        characterStats cStats = playerTarget.GetComponent<characterStats>();  
        cStats.takeDamage(burnDmg);
    }
    public void  OnTriggerEnter2D(Collider2D other){
         if(other.gameObject.tag == "character"){
            Debug.Log("BURNING PLAYER");
            playerTarget = other.gameObject;
            InvokeRepeating("dealDmg", waitForBurn, burnInterval);
         }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "character"){
            CancelInvoke("dealDmg");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       Destroy(this.gameObject, destroyTime);
    }

}
