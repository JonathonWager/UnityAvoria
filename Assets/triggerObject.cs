using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerObject : MonoBehaviour
{
    bool entered =false;
    bool startedPoolingFlag = false;

    List<string> greasy = new List<string>();

    public string[] list1;

    public List<Vector3> vector3List;   
    // Start is called before the first frame update
    void Start()
    {
        list1 = new string[] {"spinAttacker-0", "charger-5", "charger-3,spinAttacker-0,spinAttacker-0,spinAttacker-0,spinAttacker-0"};
          
        // Now list1 is initialized with the specified string values.
    }
    void Pooling(string[] enemyList){
        float delayCount = 0;
        foreach (string e in enemyList)
        {
            string[] atts = e.Split(',');
            foreach(string t in atts){
                string[] atts2 = t.Split('-');
                    greasy.Add(atts2[0]);
                    delayCount += float.Parse(atts2[1]);
                    Invoke("spawnEnemy", delayCount);    
            }
        }
    }


    void spawnEnemy(){
        Debug.Log("Spawning Enemy " + greasy[0]);
        if(Resources.Load(greasy[0]) as GameObject != null){
            int rand = Random.Range(0, vector3List.Count - 1);
            Instantiate(Resources.Load(greasy[0]) as GameObject, new Vector3(vector3List[rand].x,vector3List[rand].y,-1f), Quaternion.identity);
        }    
        greasy.RemoveAt(0);
    }
    public void  OnTriggerEnter2D(Collider2D other){
        Debug.Log("entered");
        if(other.gameObject.tag == "character"){
            entered = true;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(entered){
            if(!startedPoolingFlag){
                 Pooling(list1);
                 startedPoolingFlag = true;
            }
           
        }
    }
}
