using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public int hp = 100;
    public int def = 10;
    public int baseAtk = 10;
    public int adjAtk = 10;
    public float range = 2f;
    private float elapsed =0f;
    public void takeDamage(decimal damage){
        hp = hp - (int)(damage * (1-(def  / 100)));
   
    }

    public int getHp(){
        return hp;
    }
    public float getRange(){
        return range;
    }
    public void setRange(float Range){
        range = Range;
    }
    public void setDamage(int dmgBuff){
        adjAtk = dmgBuff;
    }
    public decimal getadjAtk(){
        return adjAtk;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void weaponStats(Weapon curWeap){  
        adjAtk = (int)((decimal)baseAtk + (decimal)curWeap.getDamage());
        range = curWeap.getRange();
    }

  
    // Update is called once per frame
    void Update()
    {
      
        if(hp <= 0){
            Destroy(this.gameObject);
        }
          if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
              Debug.Log("test");
            Vector2 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            Ray2D ray = new Ray2D(transform.position, mouseDirection);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 20f);
            if(hits.Length > 1){
                foreach(RaycastHit2D hit in hits){
                  
                   
                     if(hit.collider != null && hit.collider.gameObject.tag == "enemy")
                    {
                        if(hit.distance <= range){ 
                            enemyStats eEnemy = hit.collider.gameObject.GetComponent<enemyStats>();
                             eEnemy.takeDamage((int)(adjAtk));
                        }
                    }
                }
            }
        }
    }
}
