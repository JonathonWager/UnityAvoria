using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneReset : MonoBehaviour
{
    public bool playerDead = false;
    public GameObject UI;
   public void ResetScene()
    {
        // Get the active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void StartReset(int score, int kills, int damage, int gold, int wave){
        playerDead = true;
         UI = GameObject.FindGameObjectWithTag("UI");
         UI.GetComponent<uiUpdater>().GameOver(score,kills,damage,gold,wave);

    }
    // Optionally, call this method in Update() or another event (e.g., button click, key press)
    void Update()
    {
        if(playerDead){
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetScene();
            }
        }
       
    }
}
