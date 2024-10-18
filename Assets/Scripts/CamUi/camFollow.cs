using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Mathf.Round(player.transform.position.x * 1000) / 1000f,Mathf.Round(player.transform.position.y * 1000) / 1000f ,0f) + new Vector3(0,0.001f,-10f);
        
    }

    
}
