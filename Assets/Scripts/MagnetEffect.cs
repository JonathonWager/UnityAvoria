using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    public bool isActive = false;
    GameObject player;
     public float maxSpeed = 2.0f;  // Starting speed of the movement


    private float currentSpeed;  //
    float distance,magnetDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void MagnetStart(GameObject target,float targetDistance, float magnetRange){
        player = target;
        isActive = true;
        distance = targetDistance;
        magnetDistance = magnetRange;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive){
            currentSpeed = (1 - (distance/magnetDistance)) * maxSpeed;
            Debug.Log(currentSpeed);
            // Move the current object toward the player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
    }
}
