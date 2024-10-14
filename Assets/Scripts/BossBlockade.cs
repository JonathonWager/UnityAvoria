using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NavMeshPlus.Components;
public class BossBlockade : MonoBehaviour
{
    public GameObject UI;
    private bool isActive = false;
    private GameObject player;
    private NavMeshSurface navMeshSurface;
    public int miniBossCount = 0;
    public void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("character") && miniBossCount == 0)
        {
            player = other.gameObject;
            isActive =true;
            ToggleChildByName(UI,"BossBlockade", true);
        }
    }
     public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("character")&& miniBossCount == 0)
        {
            ToggleChildByName(UI,"BossBlockade", false);
                        isActive = false;
        }
    }
     public void ToggleChildByName(GameObject parent, string childName, bool isActive)
    {
        Transform childTransform = parent.transform.Find(childName);
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning($"Child with name '{childName}' not found.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        miniBossCount = GameObject.FindGameObjectsWithTag("MiniBossSpawner").Length;

    }
    public void ResetBarrier(){
         this.gameObject.SetActive(true);
        this.GetComponent<NavMeshModifier>().area = UnityEngine.AI.NavMesh.GetAreaFromName("Not Walkable");
        foreach (Transform child in transform)
        {  
            child.gameObject.GetComponent<NavMeshModifier>().area = UnityEngine.AI.NavMesh.GetAreaFromName("Not Walkable");
        } 
         GameObject navMeshObject = GameObject.FindGameObjectWithTag("NavMesh");
        if (navMeshObject != null)
        {
            // Access the NavMeshSurface component
            navMeshSurface = navMeshObject.GetComponent<NavMeshSurface>();

            if (navMeshSurface != null)
            {
                // Rebake the NavMesh
                navMeshSurface.BuildNavMesh();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive){
            if (Input.GetKeyDown(KeyCode.F)){
                if(miniBossCount == 0){
                    this.GetComponent<NavMeshModifier>().area = UnityEngine.AI.NavMesh.GetAreaFromName("Walkable");
                    foreach (Transform child in transform)
                    {  
                        child.gameObject.GetComponent<NavMeshModifier>().area = UnityEngine.AI.NavMesh.GetAreaFromName("Walkable");
                    } 
                    GameObject navMeshObject = GameObject.FindGameObjectWithTag("NavMesh");
                    if (navMeshObject != null)
                    {
                        // Access the NavMeshSurface component
                        navMeshSurface = navMeshObject.GetComponent<NavMeshSurface>();

                        if (navMeshSurface != null)
                        {
                            // Rebake the NavMesh
                            navMeshSurface.BuildNavMesh();
                            Debug.Log("NavMesh has been rebaked!");
                        }
                        else
                        {
                            Debug.LogError("NavMeshSurface component not found on the NavMesh GameObject.");
                        }
                    }
                    else
                    {
                        Debug.LogError("No GameObject found with the tag 'NavMesh'.");
                    }
                    this.gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.C)){
                ToggleChildByName(UI,"BossBlockade", false);
                isActive = false;
            }
        }
    }
}
