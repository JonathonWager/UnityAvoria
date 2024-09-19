using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRotation : MonoBehaviour
{
    public float rotationSpeed = 45f; 

    void Update()
    {
         transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
