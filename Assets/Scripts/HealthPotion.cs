using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int heal;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            other.gameObject.GetComponentInChildren<characterStats>().hp += (int)(heal * GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>().potionIncreaseMofider);
            Destroy(this.gameObject);
        }
    }
    public void Start(){
        Destroy(this.gameObject,15f);
    }
}
