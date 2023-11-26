using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    private GameObject player;

    private Vector3 storageLocation;
    public bool isShooting{get;set;} =false;
    float elapsed = 0f;
    public float dmgSpeed = 1f;
    
    // Start is called before the first frame update
    void Update(){
        elapsed += Time.deltaTime;
        if(isShooting){
            ShootLaser();
        }else{
            Ray2D ray = new Ray2D();
        }
        
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");
        m_transform = GetComponent<Transform>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 2; 
    }
    Vector3 CalculateLocationInDirection(Vector3 targetLocation, float xDistance)
    {
        // Get the current position of the GameObject
        Vector3 currentPosition = transform.position;

        // Calculate the direction from the current position to the target location
        Vector3 directionToTarget = (targetLocation - currentPosition).normalized;

        // Calculate the new location by adding the direction multiplied by the distance
        Vector3 newLocation = targetLocation + directionToTarget * xDistance;

        return newLocation;
    }
    // Update is called once per frame
    void ShootLaser()
    {

        Ray2D ray = new Ray2D(transform.position, (storageLocation - laserFirePoint.position).normalized );
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 50f);
      
        if(elapsed >= dmgSpeed){
            elapsed = 0f;
            foreach (RaycastHit2D hit in hits)
            {
                    Debug.Log("Hit  " + hit.collider.gameObject.name);
                if (hit.collider != null && hit.collider.gameObject.tag == "character")
                {
                    Debug.Log("HIT PLAYER");
                    characterStats cStats = hit.collider.gameObject.GetComponent<characterStats>();
                    cStats.takeDamage(20);
                }
            }
        }
        
        Draw2DRay(laserFirePoint.position, storageLocation);
    }
    public void initLaser(){
        getTarget();
        isShooting = true;
        m_lineRenderer.positionCount = 2; 
    }
     public void deinitLaser(){
        isShooting = false;
        m_lineRenderer.positionCount = 0;
    }
    public void getTarget(){
        storageLocation = CalculateLocationInDirection(player.transform.position, 20f);
    }
    void Draw2DRay(Vector3 startPos, Vector3 endPos){
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1,endPos);
    }
}
