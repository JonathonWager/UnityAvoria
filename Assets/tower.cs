using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
     public LineRenderer lineRenderer;
     public GameObject beegBoss;
    // Start is called before the first frame update
    void Start()
    {
        beegBoss = GameObject.Find("beegBoss");
       lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; 
         
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, beegBoss.transform.position);
    }
}
