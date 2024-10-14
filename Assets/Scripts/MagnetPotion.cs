using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPotion : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            other.gameObject.GetComponentInChildren<EffectsManager>().MagnetEnable();
            Destroy(this.gameObject);
        }
    }

}
