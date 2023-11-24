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
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("character").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        towerArray = GameObject.FindGameObjectWithTag("towerArray");
    }

    void initTowers(){
        int count = 0;
        
        foreach (Transform child in towerArray.transform)
        {
            towers[count] = child.gameObject;
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
