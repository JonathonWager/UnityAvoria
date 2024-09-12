using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NavMeshPlus.Components;
public class RoomBlockade : MonoBehaviour
{
     public GameObject UI;
    public int blockadeCost = 500;
    private bool isActive = false;
    private GameObject player;
    private NavMeshSurface navMeshSurface;
    public void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("character"))
        {
            player = other.gameObject;
            isActive =true;
            ToggleChildByName(UI,"Blockade", true);
            Transform childTransform = UI.transform.Find("Blockade").transform.Find("BlockadeAmount");
            childTransform.gameObject.GetComponent<Text>().text = blockadeCost.ToString();
        }
    }
     public void OnTriggerExit2D(Collider2D other)
    {
        ToggleChildByName(UI,"Blockade", false);
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
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            if (Input.GetKeyDown(KeyCode.B)){
                if(player.GetComponent<characterStats>().gold > blockadeCost){
                    player.GetComponent<characterStats>().gold -= blockadeCost;
                    this.GetComponent<NavMeshModifier>().area = UnityEngine.AI.NavMesh.GetAreaFromName("Walkable");
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
                    Destroy(this.gameObject);
                }
            }
            if (Input.GetKeyDown(KeyCode.C)){
                ToggleChildByName(UI,"Blockade", false);
                isActive = false;
            }
        }
    }
}
