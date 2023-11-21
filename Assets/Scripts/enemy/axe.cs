using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe : MonoBehaviour
{
    private int damage = 20;
    private float range = 3f;
    // Start is called before the first frame update
    void Start()
    {
        spinAttcker sStats = GetComponentInParent<spinAttcker>();
        damage = sStats.getDamage();
        range = sStats.getRange();

        Vector3 initialPosition = transform.position;

        // Set the new scale
        transform.localScale = new Vector3(transform.localScale.x, range, transform.localScale.z);

        // Calculate the position adjustment based on the difference in Y scale
        float scaleYDifference = (transform.localScale.y - 1.0f) / 2.0f;
        Vector3 positionAdjustment = transform.up * scaleYDifference;

        // Move the object back to align the base with the reference object
        transform.position = initialPosition - positionAdjustment;
    }
    public void  OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "character"){
            characterStats cStats = other.gameObject.GetComponent<characterStats>();
            cStats.takeDamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
