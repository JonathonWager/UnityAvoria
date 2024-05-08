using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransitioner : MonoBehaviour
{
    bool swapFlag = true;
    public int rCount = 12;
     public void  OnTriggerEnter2D(Collider2D other){
        Debug.Log("entered");
        if(other.gameObject.tag == "character"){
            if(swapFlag){
                swapFlag = false;
                int rand = Random.Range(0, rCount);
                string rId = "ForestRoute_" + rand;
                rId = "ForestRoute_1";
                SceneManager.LoadScene(rId);
            }
            //swapScene
         

        }
        
    }

}
