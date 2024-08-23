using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private int gold;

    public void Initialize(int amount)
    {
        gold = amount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test");
        if(other.gameObject.tag == "character"){
            other.gameObject.GetComponent<characterStats>().addGold(gold);
            Destroy(gameObject);
        }
       
    }
}
