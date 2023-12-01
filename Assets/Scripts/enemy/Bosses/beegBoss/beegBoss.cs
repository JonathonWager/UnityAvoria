using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class beegBoss : MonoBehaviour
{

    // Tower-related variables
    private GameObject towerArray;
    private GameObject[] towers = new GameObject[4];
    private int towerCount = 4;  // Initial tower count

    // Target and navigation variables
    [SerializeField] Transform target;
    NavMeshAgent agent;

    // Skelly Nest spawn variables
    public float skellySpawnTimer = 8f;
    public float skellyNestAccuracy = 3f;
    public float fazeOneRange = 8f;
    public GameObject skellyNest;

    // Faze Two variables
    public bool pastHalf{get; set;} = false;
    bool pastHalfToggle = false;
    bool startedFour = false;
    private bool fazeOne, fazeTwo, fazeThree, fazeFour = false;

    private float fazeTwoTimer = 10f;
    public float FazeTwoShootSpeed = 3f;
    public float FazeTwoVulnerableTime = 2f;

    public float FazeTwoDistance = 5f;
    public bool isVulnerble{get; set;} = false;

    // Timer and Rigidbody variables
    float elapsed = 0f;
    private Rigidbody2D rb;

    // Faze Two initialization
    void startFazeTwo()
    {
        fazeOne = false;
        fazeTwo = true;
        Invoke("stopFazeTwo", fazeTwoTimer);

    }

    // Faze Two termination
    void stopFazeTwo()
    {
        fazeOne = true;
        fazeTwo = false;
        elapsed = 0f;
        laser lStats = gameObject.GetComponent<laser>();
        lStats.DeinitLaser();
        isVulnerble = false;
    }

    // Check the number of towers and trigger Faze Two if different
    void towerCheck()
    {
        int x = 0;
        for (int i = 0; i < 4; i++)
        {
            if (towers[i] != null)
            {
                x++;
            }
        }
        if (x != towerCount)
        {
            towerCount = x;
            startFazeTwo();
        }
    }

    // Initialization
    void Start()
    {
        // Initialize variables
        target = GameObject.FindGameObjectWithTag("character").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        towerArray = GameObject.FindGameObjectWithTag("towerArray");
        elapsed = skellySpawnTimer;
        rb = GetComponent<Rigidbody2D>();
        fazeFour = true;
        initTowers();
    }

    // Initialize tower array
    void initTowers()
    {
        int count = 0;
        foreach (Transform child in towerArray.transform)
        {
            towers[count] = child.gameObject;
            count++;
        }
    }

    // Spawn Skelly Nest based on distance and timer
    void spawnSkellyNest()
    {
        float x = Random.Range(target.position.x - skellyNestAccuracy, transform.position.x + skellyNestAccuracy);
        float y = Random.Range(target.position.y - skellyNestAccuracy, transform.position.y + skellyNestAccuracy);
        Instantiate(skellyNest, new Vector3(x, y, -1f), Quaternion.identity);
    }

    // Faze One behavior
    void FazeOne()
    {
        if (Vector3.Distance(transform.position, target.position) <= fazeOneRange)
        {
            if (elapsed >= skellySpawnTimer)
            {
                elapsed = 0f;
                spawnSkellyNest();
            }
            Vector3 awayDirection = transform.position - target.position;
            agent.SetDestination(transform.position + awayDirection);
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }

    // Rotate object based on velocity
    void rotateObject()
    {
        Vector2 velocity = rb.velocity;
        if (velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // Faze Two behavior
    void FazeTwo()
    {
        if (elapsed >= FazeTwoShootSpeed)
        {
            elapsed = 0f;
            laser lStats = gameObject.GetComponent<laser>();
            lStats.InitLaser();
            isVulnerble = true;
        }
        if (Vector3.Distance(transform.position, target.position) <= FazeTwoDistance)
        {
            Vector3 awayDirection = transform.position - target.position;
            agent.SetDestination(transform.position + awayDirection);
        }else
        {
            agent.SetDestination(target.position);
        }

    }
    void FazeThree(){
         if (elapsed >= FazeTwoShootSpeed)
        {
            elapsed = 0f;
            laser lStats = gameObject.GetComponent<laser>();
            lStats.InitLaser();
            isVulnerble = true;
        }
         if (Vector3.Distance(transform.position, target.position) <= fazeOneRange)
        {
            if (elapsed >= skellySpawnTimer)
            {
                elapsed = 0f;
                spawnSkellyNest();
            }
            Vector3 awayDirection = transform.position - target.position;
            agent.SetDestination(transform.position + awayDirection);
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }
    void FazeFour(){

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        towerCheck();
        elapsed += Time.deltaTime;

        if(!pastHalfToggle){
            if(pastHalf){
                fazeOne = false;
                 fazeTwo = false;
                fazeThree = true;
                pastHalfToggle = true;
            }
        }
        
        if (fazeOne)
        {
            FazeOne();
        }
        else if (fazeTwo)
        {
            FazeTwo();
        }else if(fazeThree){
            FazeThree();
        }else if(fazeFour){
            if(!startedFour){
                Debug.Log("starting 4");
                startedFour = true;
                laser lStats = gameObject.GetComponent<laser>();
                lStats.startPartyTime();
            }
            FazeFour();
        }
    }
}