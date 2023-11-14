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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        if(elapsed >= spawnTime){
            if(Random.Range(0,1)==1){
                float x = Random.Range(minRadius, maxRadius);
            }else{
                float x = Random.Range(-minRadius,-maxRadius);
            }
            if(Random.Range(0,1)==1){
                float y = Random.Range(minRadius, maxRadius);
            }else{
                float y = Random.Range(-minRadius,-maxRadius);
            }
            Instantiate(enemy, this.transform.position + new Vector3(x, y, 0), Quaternion.identity);
            elapsed = 0f;
        }
        
    }
}
