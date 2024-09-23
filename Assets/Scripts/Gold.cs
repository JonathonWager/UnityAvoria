using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
     private int gold;
    private GameObject player;
     public float baseSpeed = 2.0f;  // Starting speed of the movement
    public float speedIncreaseRate = 0.1f;  // Amount to increase speed over time

    private float currentSpeed;  //

    public void Initialize(int amount)
    {
        gold = amount;
    }
    public void Start(){
        player = GameObject.FindGameObjectWithTag("character");
        currentSpeed = baseSpeed;
    }
    void Update(){
         if (player != null)
        {
            // Gradually increase the speed
            currentSpeed += speedIncreaseRate * Time.deltaTime;

            // Move the current object toward the player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            other.gameObject.GetComponent<characterStats>().addGold(gold);
            Destroy(gameObject);
        }
       
    }
}
