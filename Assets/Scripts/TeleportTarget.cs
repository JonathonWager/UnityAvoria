using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; 
public class TeleportTarget : MonoBehaviour
{
    public  float maxDistance = 0.5f;
    public bool isValid = false;

    Light2D childLight;

    public List<string> tags = new List<string> { "wall" };
    GameObject player;
    LineRenderer lr;
    public float range;
    Vector2 direction;
     float distance;
    // Start is called before the first frame update
    void Start()
    {
        childLight = GetComponentInChildren<Light2D>();
        player = GameObject.FindGameObjectWithTag("character");
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {  
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorWorldPosition.z = 0;

           // Calculate the direction from the player to the cursor (target)
        direction = (cursorWorldPosition - player.transform.position).normalized;

        // Calculate the distance to the target (cursor)
        distance = Vector2.Distance(player.transform.position, cursorWorldPosition);


        
        CheckRaycastDrawLine();
        if(isValid){
            childLight.color = Color.cyan;
        }else{
            childLight.color = Color.red;
        }
    }

    void CheckRaycastDrawLine()
    {
     

        // Perform the raycast from the player's position to the target's position and get all hits
        RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position, direction, distance);

        // Assume the path is clear initially
        isValid = true;

        // Check each hit
        foreach (RaycastHit2D hit in hits)
        {
            // If the hit collider has an ignored tag, skip it
            if (IsTag(hit.collider.tag))
            {
                Debug.Log("Hit something: " + hit.collider.name);
                isValid = false;
                break;
           }

        }

        // Draw the ray for debugging
        Debug.DrawRay(player.transform.position, direction * distance, Color.red);

        //Line Renderer
         Vector2 endPosition;
        if(distance > range){
            endPosition = (Vector2)player.transform.position + (direction * range);
        }else{
            endPosition = (Vector2)player.transform.position + (direction * distance);
        }
      
        transform.position = endPosition;
        // Set the LineRenderer positions
        lr.positionCount = 2; // We need two points for a line
        lr.SetPosition(0, player.transform.position); // Start point
        lr.SetPosition(1, endPosition);   // End point

    }


        private bool IsTag(string tag)
    {
        return tags.Contains(tag);
    }
  

}
