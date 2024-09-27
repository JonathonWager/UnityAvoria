using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEffect : MonoBehaviour
{
    public void StartLevelEffect(){
        this.gameObject.SetActive(true);
        Invoke("StopEffect",2f);
        
    }
    void StopEffect(){
        this.gameObject.SetActive(false);
    }

}
