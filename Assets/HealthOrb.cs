using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    public int health;
    public string targetObjectName;
    GameObject target;
    public float baseSpeed = 2.0f;  // Starting speed of the movement
    public float speedIncreaseRate = 0.1f;  // Amount to increase speed over time

    public float collectDistance = 0.5f;

      private float currentSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find(targetObjectName);
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed += speedIncreaseRate * Time.deltaTime;

            // Move the current object toward the player
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, currentSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target.transform.position) <= collectDistance){
            if(target.GetComponent<bossStats>().hp < target.GetComponent<bossStats>().maxHPDontSet){
                target.GetComponent<bossStats>().hp += health;

            }
                            Destroy(this.gameObject);
        }
    }
}
