using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay=100;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    private GameObject player;

    private Vector3 storageLocation;
    public bool isShooting{get;set;} =false;
    
    // Start is called before the first frame update
    void Update(){
        if(isShooting){
            ShootLaser();
        }
        
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");
        m_transform = GetComponent<Transform>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 2; 
    }
     public Vector3 GetPointInFront(float distance)
    {
        // Get the current position and rotation of the GameObject
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        // Calculate the offset in the forward direction based on the distance
        Vector3 offset = distance * transform.forward;

        // Calculate the final position by adding the offset to the current position
        Vector3 targetPosition = currentPosition + offset;

        return targetPosition;
    }
    // Update is called once per frame
    void ShootLaser()
    {
        Debug.Log(storageLocation);
        Ray2D ray = new Ray2D(transform.position, storageLocation);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 20f);
        if (hits.Length > 1)
        {

        }       
        Draw2DRay(laserFirePoint.position, storageLocation);
    }
    public void initLaser(){
        getTarget();
        isShooting = true;
    }
    public void getTarget(){
        storageLocation = player.transform.position;
    }
    void Draw2DRay(Vector3 startPos, Vector3 endPos){
          
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1,endPos);
    }
}
