using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    public float healInterval = 2f;
    public GameObject healOrb;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnHealth", 0f, healInterval);
         
    }

    void SpawnHealth(){
        Instantiate(healOrb, transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
