using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brimonnolly : MonoBehaviour
{
    public GameObject player;
    public GameObject fire;
    public float speed;
    public float range;
    private float elapsed = 0f;
    public float attackTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        elapsed = attackTime;
        player = GameObject.FindGameObjectWithTag("character");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position) < range){
            if(elapsed >= attackTime){
                Instantiate(fire, player.transform.position, Quaternion.identity);
                elapsed = 0f;
            }
        }else{
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
         elapsed += Time.deltaTime;
    }
}
