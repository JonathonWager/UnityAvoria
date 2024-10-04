using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    // Public variables
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;

    // Private variables
    Transform m_transform;
    private GameObject player;
    private Vector3 storageLocation;
    public bool isShooting { get; set; } = false;
    float elapsed = 0f;
     float elapsed2 = 0f;
    public float dmgSpeed = 1f;
    public float targetingTime = 0.5f;
    public bool PARTYTIME { get; set; } = false;
    public float targetRotation = 1000f;
    public float partyDegreeUpdateTime = 1f;
    public float angleInc = 0.5f;
    public float laserLength = 50f;
    private Vector3 storageRotation;
    public int damage;
    List<Vector2> storageDirections;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");
        m_transform = GetComponent<Transform>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 2;
    }
    public void startPartyTime(){
        elapsed2 = dmgSpeed;
        GetTarget(); // Assuming you have a method called GetTarget()
        PARTYTIME = true;
        storageDirections = CalculateDirections(laserFirePoint.position,angleInc, targetRotation);
    }
    void PARTY(){
        if (elapsed >= partyDegreeUpdateTime && storageDirections[0] != null)
        {
            elapsed = 0f;
            storageDirections[0].Normalize();
            
            Draw2DRay(laserFirePoint.position, storageDirections[0]);
            
            storageDirections.RemoveAt(0);
        }
    }

    public List<Vector2> CalculateDirections(Vector2 gameObjectPosition, float angleIncrement, float targetRotation)
    {
        List<Vector2> directions = new List<Vector2>();

        int numDirections = (int)(targetRotation/angleIncrement);

        gameObjectPosition.Normalize();
     
        Vector3 targetDir =player.transform.position - transform.position;
        targetDir = targetDir.normalized;
        //BAD CODE VERY BAD
        bool xPos = false;
        bool yPos = false;

        if(targetDir.x > 0){
            xPos = true;
        }
        if(targetDir.y > 0){
            yPos = true;
        }
        float startAngle = 0f;
        if(xPos && yPos){
            startAngle = 0f;
        }
        if(!xPos && yPos){
            startAngle = 90f;
        }
        if(!xPos && !yPos){
            startAngle = 180f;
        }
        if(xPos && !yPos){
            startAngle = 270f;
        }
        for (int i = 0; i < numDirections; i++)
        {
            float angle = i * angleIncrement + startAngle;
      
            Vector2 direction = GetDirectionFromAngle(angle);
            directions.Add(CalculateLocationInDirection((direction + (Vector2)transform.position),laserLength));
        }

        return directions;
    }

    private Vector2 GetDirectionFromAngle(float angle)
    {
        float radianAngle = Mathf.Deg2Rad * angle;
        float x = Mathf.Cos(radianAngle);
        float y = Mathf.Sin(radianAngle);

        return new Vector2(x, y);
    }
    // Calculate a location in the direction of the target
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

    // Method to handle laser shooting logic
    void ShootLaser()
    {
        // Create a ray from the laser fire point to the calculated storage location
        Ray2D ray = new Ray2D(transform.position, (storageLocation - laserFirePoint.position).normalized);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, laserLength);

        // Check if enough time has passed to deal damage
        if (elapsed >= dmgSpeed)
        {
            elapsed = 0f;

            // Loop through all hits and damage the character if hit
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.tag == "character")
                {
                    characterStats cStats = hit.collider.gameObject.GetComponent<characterStats>();
                    cStats.takeDamage(20); // I assumed a method name change to follow conventions
                }
            }
        }

        // Draw the 2D ray using the LineRenderer
        Draw2DRay(laserFirePoint.position, storageLocation);
    }

    // Method to start shooting the laser
    public void LaserStart()
    {
        isShooting = true;
        m_lineRenderer.positionCount = 2;
    }

    // Method to initialize the laser and target
    public void InitLaser()
    {
        GetTarget();
        Invoke("LaserStart", targetingTime);
    }

    // Method to stop shooting the laser
    public void DeinitLaser()
    {
        isShooting = false;
        m_lineRenderer.positionCount = 0;
    }

    // Method to get the target's location
    public void GetTarget()
    {
        storageLocation = CalculateLocationInDirection(player.transform.position, 20f);
    }

    // Method to draw the 2D ray using LineRenderer
    void Draw2DRay(Vector3 startPos, Vector3 endPos)
    {
        m_lineRenderer.positionCount = 2;
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
       
        if(PARTYTIME){
            //Debug.Log("Debug Ray");
             Debug.DrawRay(startPos, new Vector3((endPos - startPos).normalized.x,(endPos - startPos).normalized.y,0f) * laserLength, Color.green);
             Ray2D ray = new Ray2D(startPos, new Vector3((endPos - startPos).normalized.x,(endPos - startPos).normalized.y,0f));
             RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, laserLength);
             if(elapsed2 >= dmgSpeed){
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.gameObject.tag == "character")
                    {
                        elapsed2 = 0f;
                        characterStats cStats = hit.collider.gameObject.GetComponent<characterStats>();
                        cStats.takeDamage(damage);
                    }
                }
            }
        }         
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        elapsed2 += Time.deltaTime;

        // Check if it's party time
        if (!PARTYTIME)
        {
            // Check if the laser is shooting
            if (isShooting)
            {
                ShootLaser();
            }
        }
        else
        {
            PARTY();
        }
    }
}