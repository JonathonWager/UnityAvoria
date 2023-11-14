using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brimFIre : MonoBehaviour
{
    public float burnRadius;
    public float burnTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
        this.transform.localScale += new Vector3(burnRadius,burnRadius,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
