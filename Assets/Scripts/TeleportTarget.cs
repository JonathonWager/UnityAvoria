using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; 
public class TeleportTarget : MonoBehaviour
{
    public  float maxDistance = 0.5f;
    public bool isValid = false;

    Light2D childLight;

    public List<string> ignoredTags = new List<string> { "Enemy", "Shop" };
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        childLight = GetComponentInChildren<Light2D>();
        player = GameObject.FindGameObjectWithTag("character");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorWorldPosition.z = 0;
        transform.position = cursorWorldPosition;

                

        int walkableAreaMask = 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable"); // Default walkable area mask
        if (UnityEngine.AI.NavMesh.SamplePosition(this.transform.position, out UnityEngine.AI.NavMeshHit hit, maxDistance, walkableAreaMask))
        {
            CheckRaycast();
            if(isValid){
                childLight.color = Color.cyan;
            }else{
                childLight.color = Color.red;
            }
        
        }else{
        isValid = false;
        childLight.color = Color.red;
    }
    }
    void CheckRaycast()
    {
        // Calculate the direction from the player to the cursor (target)
        Vector2 direction = (transform.position - player.transform.position).normalized;

        // Calculate the distance to the target (cursor)
        float distance = Vector2.Distance(player.transform.position, transform.position);

        // Perform the raycast from the player's position to the target's position and get all hits
        RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position, direction, distance);

        // Assume the path is clear initially
        isValid = true;

        // Check each hit
        foreach (RaycastHit2D hit in hits)
        {
            // If the hit collider has an ignored tag, skip it
            if (IsIgnoredTag(hit.collider.tag))
            {
                Debug.Log("Hit an ignored object: " + hit.collider.name);
                continue;
            }

            // If the hit collider is a valid obstacle (non-ignored), mark path as blocked
            if (hit.collider != null)
            {
                Debug.Log("Hit something: " + hit.collider.name);
                isValid = false;
                break;
            }
        }

        // Draw the ray for debugging
        Debug.DrawRay(player.transform.position, direction * distance, Color.red);
    }


        private bool IsIgnoredTag(string tag)
    {
        return ignoredTags.Contains(tag);
    }
  

}
