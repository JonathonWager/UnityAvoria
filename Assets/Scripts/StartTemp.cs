using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTemp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buildSelector;
    void Start()
    {
        
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Running");
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Resume the game by setting timeScale to 1
            Debug.Log("HIYING S");
            //Time.timeScale = 1f;

            buildSelector.GetComponent<BuildSelection>().StartBuildSelection();
            Destroy(this.gameObject);
        }
    }
}
