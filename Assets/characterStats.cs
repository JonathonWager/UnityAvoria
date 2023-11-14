using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public decimal hp = 100;
    public int speed = 5;
    public decimal def = 10;
    public decimal baseAtk = 10;
    public decimal adjAtk = 10;
    public float range = 2f;
    public void takeDamage(decimal damage){
        Debug.Log(hp);
        hp = hp - (damage * (1-(def  / 100)));
   
    }

    public int getHp(){
        return (int)hp;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void weaponStats(weapon curWeap){
        
        adjAtk = baseAtk + curWeap.getDamage();
        range = curWeap.getRange();
        Debug.Log(adjAtk);
    }
    // Update is called once per frame
    void Update()
    {
        if(hp <= 0){
            Destroy(this.gameObject);
        }
          if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            Vector2 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            Ray2D ray = new Ray2D(transform.position, mouseDirection);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 20f);
            if(hits.Length > 1){
                if(hits[1].collider != null && hits[1].collider.gameObject.tag == "enemy")
                {
                    float dist = Vector3.Distance(hits[1].collider.gameObject.transform.position, transform.position);
                    if(dist <= range){
                        capsuleEnemy cEnemy = hits[1].collider.gameObject.GetComponent<capsuleEnemy>();
                         cEnemy.takeDamage((int)(adjAtk));
                        //Destroy(hits[1].collider.gameObject);
                    }
                

                }
            }
        }
    }
}
