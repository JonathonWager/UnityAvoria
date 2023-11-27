using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawBlade : MonoBehaviour
{
    public bool toEnd = true;
    public bool toStart = false;
    public Vector3 startLocation;
    public Vector3 endLocation;
    public float speed = 5f;
    public GameObject player;
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;
        player = GameObject.FindGameObjectWithTag("character");
    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
        characterStats cStats = player.GetComponent<characterStats>();
        cStats.takeDamage(damage);

    }
    // Update is called once per frame
    void Update()
    {
        if(toEnd){
              transform.position = Vector3.MoveTowards(transform.position, endLocation, speed * Time.deltaTime);
        }
        if(toStart){
            transform.position = Vector3.MoveTowards(transform.position, startLocation, speed * Time.deltaTime);
        }
        if(transform.position == startLocation){
            toEnd = true;
            toStart = false;
        }
        if(transform.position == endLocation){
            toEnd = false;
            toStart = true;
        }

    }
}
