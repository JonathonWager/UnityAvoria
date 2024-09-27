using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    GameObject UI;
    void Start(){
        UI = GameObject.FindGameObjectWithTag("UI");
    }
    void Update()
    {
        // Check if the player presses the Escape key or P key to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            // Unpause the game
            foreach(Transform child in UI.transform){
                if(child.gameObject.name == "GamePaused"){
                    child.gameObject.SetActive(false);
                }
            }
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
             foreach(Transform child in UI.transform){
                if(child.gameObject.name == "GamePaused"){
                    child.gameObject.SetActive(true);
                }
            }
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}
