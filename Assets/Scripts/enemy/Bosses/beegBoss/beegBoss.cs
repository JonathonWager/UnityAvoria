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
    [SerializeField] private Transform target;
    private NavMeshAgent agent;

    // Skelly Nest spawn variables
    [Header("Skelly Nest Settings")]
    public float skellySpawnTimer = 8f;
    public float skellyNestAccuracy = 3f;
    public float fazeOneRange = 8f;
    public GameObject skellyNest;

    // Faze Two variables
    [Header("Faze Two Settings")]
    public float fazeTwoTimer = 10f;
    public float FazeTwoShootSpeed = 3f;
    public float FazeTwoVulnerableTime = 2f;
    public float FazeTwoDistance = 5f;
    public bool isVulnerable { get; set; } = false;

    // Faze Four variables
    [Header("Faze Four Settings")]
    public float FazeFourTime = 4f;
    public float FazeFourResetTime = 4f;

    // Timer and Rigidbody variables
    private float elapsed = 0f;
    private Rigidbody2D rb;

    // Faze flags
    private bool fazeOne, fazeTwo, fazeThree, fazeFour = false;
    private bool pastHalfToggle, pastQuartToggle, startedFour = false;
    public bool  pastHalf {get; set;} = false;    // Faze Two initialization
    public bool  pastQuart{get; set;} = false; 
    private void StartFazeTwo()
    {
        fazeOne = false;
        fazeTwo = true;
        Invoke("StopFazeTwo", fazeTwoTimer);
    }

    // Faze Two termination
    private void StopFazeTwo()
    {
        fazeOne = false;
        if(!fazeThree){
            fazeOne = true;
        }
        fazeTwo = false;
        elapsed = 0f;
        laser lStats = gameObject.GetComponent<laser>();
        lStats.DeinitLaser();
        isVulnerable = false;
    }

    // Check the number of towers and trigger Faze Two if different
    private void TowerCheck()
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
            StartFazeTwo();
        }
    }

    // Initialization
    private void Start()
    {
        // Initialize variables
        target = GameObject.FindGameObjectWithTag("character").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        towerArray = GameObject.FindGameObjectWithTag("towerArray");
        elapsed = skellySpawnTimer;
        rb = GetComponent<Rigidbody2D>();
        fazeOne = true;
        InitTowers();
    }

    // Initialize tower array
    private void InitTowers()
    {
        int count = 0;
        foreach (Transform child in towerArray.transform)
        {
            towers[count] = child.gameObject;
            count++;
        }
    }

    // Spawn Skelly Nest based on distance and timer
    private void SpawnSkellyNest()
    {
        float x = Random.Range(target.position.x - skellyNestAccuracy, transform.position.x + skellyNestAccuracy);
        float y = Random.Range(target.position.y - skellyNestAccuracy, transform.position.y + skellyNestAccuracy);
        Instantiate(skellyNest, new Vector3(x, y, -1f), Quaternion.identity);
    }

    // Faze One behavior
    private void FazeOne()
    {
        if (Vector3.Distance(transform.position, target.position) <= fazeOneRange)
        {
            if (elapsed >= skellySpawnTimer)
            {
                elapsed = 0f;
                SpawnSkellyNest();
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
    private void RotateObject()
    {
        Vector2 velocity = rb.velocity;
        if (velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // Faze Two behavior
    private void FazeTwo()
    {
        if (elapsed >= FazeTwoShootSpeed)
        {
            elapsed = 0f;
            laser lStats = gameObject.GetComponent<laser>();
            lStats.InitLaser();
            isVulnerable = true;
        }
        if (Vector3.Distance(transform.position, target.position) <= FazeTwoDistance)
        {
            Vector3 awayDirection = transform.position - target.position;
            agent.SetDestination(transform.position + awayDirection);
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }

    // Faze Three behavior
    private void FazeThree()
    {
        if (elapsed >= FazeTwoShootSpeed)
        {
            elapsed = 0f;
            laser lStats = gameObject.GetComponent<laser>();
            lStats.InitLaser();
            isVulnerable = true;
        }
        if (Vector3.Distance(transform.position, target.position) <= fazeOneRange)
        {
            if (elapsed >= skellySpawnTimer)
            {
                elapsed = 0f;
                SpawnSkellyNest();
            }
            Vector3 awayDirection = transform.position - target.position;
            agent.SetDestination(transform.position + awayDirection);
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }

    // Start Faze Four
    private void StartFazeFour()
    {
        fazeOne = false;
        fazeTwo = false;
        fazeThree = false;
        startedFour = false;
        fazeFour = true;
    }

    // Stop Faze Four
    private void StopFazeFour()
    {
        fazeOne = false;
        fazeTwo = false;
        fazeThree = true;
        startedFour = true;
        fazeFour = false;
        Invoke("StartFazeFour", FazeFourResetTime);
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log($"Faze 1: {fazeOne} | Faze 2: {fazeTwo} | Faze 3: {fazeThree} | Faze 4: {fazeFour}");
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        TowerCheck();
        elapsed += Time.deltaTime;

        if (!pastHalfToggle)
        {
            if (pastHalf)
            {
                fazeOne = false;
                fazeTwo = false;
                fazeThree = true;
                fazeFour = false;
                pastHalfToggle = true;
            }
        }
        if (!pastQuartToggle)
        {  
            if (pastQuart)
            {
                Debug.Log("starting 4");
                fazeOne = false;
                fazeTwo = false;
                fazeThree = false;
                fazeFour = true;
                pastQuartToggle = true;
            }
        }

        if (fazeOne)
        {
            FazeOne();
        }
        else if (fazeTwo)
        {
            FazeTwo();
        }
        else if (fazeThree)
        {
            FazeThree();
        }
        else if (fazeFour)
        {
            if (!startedFour)
            {
                Debug.Log("Starting Faze Four");
                startedFour = true;
                laser lStats = gameObject.GetComponent<laser>();
                lStats.startPartyTime();
                
                Instantiate(Resources.Load("Explosion") as GameObject, transform.position, Quaternion.identity);
                Invoke("StopFazeFour", FazeFourTime);
            }
        }
    }
}