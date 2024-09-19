using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int heal;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            other.gameObject.GetComponentInChildren<characterStats>().hp += heal;
            Destroy(this.gameObject);
        }
    }
}
