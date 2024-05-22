using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class shotgunShot : MonoBehaviour
{
    
  private Vector3 direction;
    // Target position for the arrow
    private Vector3 targetPosition;
    public GameObject bullet;
    public float maxRotationAngle = 180f;
      public float destroyTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;
        direction = (targetPosition - transform.position).normalized;

        for (int i = 0; i <  Random.Range(5, 12); i++)
        {
          
            GameObject instance = Instantiate(bullet, transform.position, Quaternion.identity);

            // Calculate the initial rotation based on the initial direction
            Quaternion initialRotation = Quaternion.LookRotation(Vector3.forward, direction);
            

            // Calculate a random rotation within the specified range (-maxRotationAngle to maxRotationAngle)
            float randomAngle = Random.Range(-maxRotationAngle, maxRotationAngle);

            // Create a random rotation quaternion around the Z axis
            Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);

            // Combine the initial rotation with the random rotation
            instance.transform.rotation = initialRotation * randomRotation;
        }
         Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
