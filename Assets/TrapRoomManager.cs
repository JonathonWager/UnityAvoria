using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    WaveManager waveManager;
    public int StartRound = 5;
    public int trapIncreaseInterval = 10;
    public int spawnAmount = 1;
    private List<GameObject> childGameObjects;
    bool started = false;
    int waveStorage =0;
    private List<int> childStorage = new List<int>();
    int waveCounter = 0;
    void Start()
    {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        childGameObjects = GetAllChildGameObjects();
        
        // For demonstration, print the names of the child GameObjects
        foreach (GameObject child in childGameObjects)
        {
            Debug.Log("Child GameObject: " + child.name);
        }
    }
    private List<GameObject> GetAllChildGameObjects()
    {
        // Create a new list to store child GameObjects
        List<GameObject> children = new List<GameObject>();

        // Iterate through all the child transforms of this GameObject
        foreach (Transform child in transform)
        {
            // Add the child GameObject to the list
            children.Add(child.gameObject);
        }

        return children;
    }
    public void UpdateChildrenDmg(int modifier){
        foreach(GameObject child in GetAllChildGameObjects()){
             foreach (Transform saw in child.transform)
            {
                 saw.gameObject.GetComponent<sawBlade>().damage  +=  modifier;
            }
           
        }
    }
    // Update is called once per frame

    void Update()
    {
        if(waveManager.wave == StartRound){
            started = true;
        }
        if(started){
            if(waveStorage != waveManager.wave){
                waveCounter += 1;

                if(waveCounter == trapIncreaseInterval){
                    spawnAmount++;
                    waveCounter = 0;
                }

                foreach(int child in childStorage){
                    childGameObjects[child].gameObject.SetActive(false);
                }
                
               
                  
                waveStorage = waveManager.wave;
                for(int i = 0; i < spawnAmount; i++){
                     int rand = Random.Range(0, childGameObjects.Count);
                    childStorage.Add(rand);
                    childGameObjects[rand].gameObject.SetActive(true);
                }
               
            }
        }
        
    }
}
