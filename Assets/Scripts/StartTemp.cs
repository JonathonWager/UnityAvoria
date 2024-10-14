using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTemp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Resume the game by setting timeScale to 1
            Time.timeScale = 1f;
            Destroy(this.gameObject);
        }
    }
}
