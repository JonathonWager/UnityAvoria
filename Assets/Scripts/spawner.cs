using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    
    public float maxRadius = 10f;
    public float minRadius = 2f;
    public float spawnTime = 5f;
     float elapsed = 0f;
     float x,y;
     int rand ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        if(elapsed >= spawnTime){
            rand = Random.Range(0,10);
            if(rand>5){
                x = Random.Range(minRadius, maxRadius);
            }else{
                x = Random.Range(-minRadius,-maxRadius);
            }
            rand = Random.Range(0,10);
            if(rand>5){
                y = Random.Range(minRadius, maxRadius);
            }else{
                y = Random.Range(-minRadius,-maxRadius);
            }
            Instantiate(enemy, this.transform.position + new Vector3(x, y, 0), Quaternion.identity);
            elapsed = 0f;
        }
        
    }
}
