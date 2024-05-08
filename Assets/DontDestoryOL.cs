using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryOL : MonoBehaviour
{
   private static GameObject playerInstance;

   private void Awake()
    {
        if (playerInstance == null)
        {
            // If no instance exists, make this the instance and don't destroy it when loading a new scene
            playerInstance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameObject
            Destroy(gameObject);
        }
    }
}
