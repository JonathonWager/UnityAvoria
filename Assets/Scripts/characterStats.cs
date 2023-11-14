using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public decimal hp = 100;
    public int speed = 5;
    public decimal def = 10;
    public decimal atk = 10;


    public void takeDamage(decimal damage){
        Debug.Log(hp);
        hp = hp - (damage * (1-(def  / 100)));
   
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            Vector2 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            Ray2D ray = new Ray2D(transform.position, mouseDirection);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 20f);
            if(hits.Length > 1){
                if(hits[1].collider != null && hits[1].collider.gameObject.tag == "enemy")
                {
                    float dist = Vector3.Distance(hits[1].collider.gameObject.transform.position, transform.position);
                    if(dist <= 2f){
                        Destroy(hits[1].collider.gameObject);
                    }
                

                }
            }
        }
    }
}
