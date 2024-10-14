using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastTower : MonoBehaviour
{
    GameObject player;

    public float distanceRange = 10f;

    public int damage = 10;
    public GameObject towerShot;
    public GameObject spawnPoint;
    bool canFire = false;
    public float speedBuff;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");

    }

    public void Fire(){
        if(canFire){
            GameObject shot = Instantiate(towerShot, spawnPoint.transform.position, Quaternion.identity);
            shot.GetComponent<TowerShot>().damage =  (damage + this.gameObject.GetComponent<enemyStats>().dmgBuff);
            shot.GetComponent<TowerShot>().speed += speedBuff;
        }
         
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(spawnPoint.transform.position,player.transform.position) < distanceRange){
            canFire = true;
        }else{
            canFire =false ;
        }
    }
}
