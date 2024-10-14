using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinePotion : MonoBehaviour
{
    public float slowAmount;
    public float slowDuration;
    public float vineRange;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            other.gameObject.GetComponentInChildren<EffectsManager>().VinesEnable(slowAmount,slowDuration,vineRange);
            Destroy(this.gameObject);
        }
    }
}
