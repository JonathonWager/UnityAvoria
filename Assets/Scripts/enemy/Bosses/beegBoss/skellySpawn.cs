using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class skellySpawn : MonoBehaviour
{
    public GameObject skelly;
    public float spawnRadius = 3f;
    public int spawnCount = 5;

    public float spawnSpeed = 1f;
    private int spawnCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
         skelly = Resources.Load("skely") as GameObject;
         UnityEngine.Rendering.Universal.Light2D spotlight = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        if (spotlight != null)
        {
            spotlight.pointLightOuterRadius = spawnRadius;
        }
        InvokeRepeating("spawnSkellys", 0f,spawnSpeed );
    }

    void spawnSkellys(){
        Debug.Log(spawnCounter + " == " + spawnCount);
        if(spawnCounter == spawnCount -1){
            Destroy(gameObject);
        }
        float x = Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius);
        float y = Random.Range(transform.position.y - spawnRadius, transform.position.y + spawnRadius);
        Instantiate(skelly, new Vector3(x,y,-1f), Quaternion.identity);
        spawnCounter++;
        
    }
}
