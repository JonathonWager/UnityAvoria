using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShopSpawn : MonoBehaviour
{
    
    public List<Vector3> vector3List; 
    public Transform player;
    private int waveCounter = 0;
    public int spawnOnCharacterInt = 3;
    public void UpdateShop(){
        waveCounter++;
        if(waveCounter == spawnOnCharacterInt){
            waveCounter = 0;
            float closestDistance = Mathf.Infinity;
            Vector3 closestPosition = Vector3.zero;
            foreach(Vector3 position in vector3List){
                float distance = Vector3.Distance(player.position, position);
                if (distance < closestDistance)
                {
                    closestDistance = distance; // Update closest distance
                    closestPosition = position;
                }
            }
            this.transform.position = closestPosition;
        }else{
            int rand = Random.Range(0, vector3List.Count);
            this.transform.position = vector3List[rand];
        }
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
