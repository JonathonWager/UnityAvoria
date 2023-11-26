using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class beegBoss : MonoBehaviour
{
    private GameObject towerArray;
    private GameObject[] towers = new GameObject[4];

    [SerializeField] Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    public float skellySpawnTimer = 8f; 

    public float fazeOneRange = 8f;
    public GameObject skellyNest;

    //0 = best accuracy 
    public float skellyNestAccuracy =3f; 

    float elapsed = 0f;
    private Rigidbody2D rb;

    private bool fazeOne,fazeTwo = false;

    bool setLaser = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("character").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        towerArray = GameObject.FindGameObjectWithTag("towerArray");
        elapsed = skellySpawnTimer;
        rb = GetComponent<Rigidbody2D>();
        fazeOne = true;
    }

    void initTowers(){
        int count = 0;
        
        foreach (Transform child in towerArray.transform)
        {
            towers[count] = child.gameObject;
            count++;
        }
    }
    void spawnSkellyNest(){
        float x = Random.Range(target.position.x - skellyNestAccuracy, transform.position.x + skellyNestAccuracy);
        float y = Random.Range(target.position.y - skellyNestAccuracy, transform.position.y + skellyNestAccuracy);
         Instantiate(skellyNest,new Vector3(x,y,-1f), Quaternion.identity);
    }
    // Update is called once per frame
    void FazeOne(){
        if(Vector3.Distance(transform.position, target.position) <= fazeOneRange){
            if(elapsed >= skellySpawnTimer){
                elapsed = 0f;
                spawnSkellyNest();
            }
             Vector3 awayDirection = transform.position - target.position;
            // Set the destination based on the away direction
            agent.SetDestination(transform.position + awayDirection);
        }else{
            agent.SetDestination(target.position);

        }
    }
    void rotateObject(){
        Vector2 velocity = rb.velocity;
        // Check if the object is moving (velocity magnitude is greater than a small value)
        if (velocity.magnitude > 0.1f)
        {
            // Calculate the rotation angle in degrees using Mathf.Atan2
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

            // Set the object's rotation to the calculated angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void FazeTwo(){
        if(!setLaser){
            laser lStats = gameObject.GetComponent<laser>();
            lStats.initLaser();
            setLaser = true;
        }
        
    }
    void Update()
    {
        elapsed += Time.deltaTime;

       
        if(fazeOne){
            FazeOne();
        }else if(fazeTwo){
            FazeTwo();
        }
        
       
    }
}
