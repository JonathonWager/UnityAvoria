using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeDropObject : MonoBehaviour
{
    public float nukeExpanTime;
    public GameObject explosionPrefab;
    private GameObject explosionObject;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            explosionObject = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            explosionObject.GetComponent<NukeExplosion>().expansionTime = nukeExpanTime;
            Destroy(this.gameObject); 
        }
    }
      public void Start(){
        Destroy(this.gameObject,13f);
    }
}
