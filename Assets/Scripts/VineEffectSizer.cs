using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class VineEffectSizer : MonoBehaviour
{
    private Light2D light2D;
    public float lightSize;
    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (light2D != null)
        {
            light2D.pointLightOuterRadius = lightSize;
        }
    }
}
