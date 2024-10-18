using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float destoryTime = 5f;
    void Start()
    {
        Destroy(this.gameObject, destoryTime);
    }
}
