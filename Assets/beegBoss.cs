using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beegBoss : MonoBehaviour
{
    private GameObject towerArray;
    private GameObject[] towers = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
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
        
    }
}
