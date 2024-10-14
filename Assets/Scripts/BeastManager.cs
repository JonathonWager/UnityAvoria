using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastManager : MonoBehaviour
{
    public List<Vector3> vector3List; 
    public GameObject Tower;
    
    List<GameObject> CurrentBeasts = new List<GameObject>();
    int waveStorage;
    bool spawned = false;
    // Start is called before the first frame update
    public void SpawnBeasts(int wave, float speedBuff, int damageBuff, int hpBuff){
        waveStorage = wave;
        spawned = true;
        foreach(Vector3 vec in vector3List){
            GameObject beast = Instantiate(Tower, vec, Quaternion.identity);
            enemyStats eStats = beast.GetComponent<enemyStats>();
            eStats.hp += hpBuff;
            eStats.dmgBuff += damageBuff;
            beast.GetComponent<BeastTower>().speedBuff = speedBuff;

            CurrentBeasts.Add(beast);
        }
    }
    public void DeleteBeasts(){
        foreach(GameObject beast in CurrentBeasts){
            Destroy(beast);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
