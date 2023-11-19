using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public int hp = 100;
    public int def = 10;
    public int baseAtk = 10;
    public int adjAtk{get; set;} = 10;
    public float range{get; set;} = 2f;

    public GameObject rangeObject{get; set;}
    public Weapon currentSelectedWeapon{get; set;}

    private bool canFire = true;
    public void takeDamage(decimal damage){
        hp = hp - (int)(damage * (1-(def  / 100)));
   
    }

    public int getHp(){
        return hp;
    }
   
    public void setDamage(int dmgBuff){
        adjAtk = dmgBuff;
    }
  
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void weaponStats(Weapon curWeap){  
        adjAtk = (int)((decimal)baseAtk + (decimal)curWeap.damage);
        range = curWeap.range;

    }
    
    void melleAttack(){
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

    void rangeAttack(){
        if(canFire){
            Instantiate(rangeObject, this.transform.position, Quaternion.identity);
            canFire = false;
            Invoke("fireReset", currentSelectedWeapon.shootInterval);
        }
         
    }
    void fireReset(){
        canFire = true;
    }
    // Update is called once per frame
    void Update()
    {
      
        if(hp <= 0){
            Destroy(this.gameObject);
        }
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            if(currentSelectedWeapon.isRanged == "M"){
                melleAttack();
            }else if(currentSelectedWeapon.isRanged == "R"){
                rangeAttack();
            }
            
        }
    }
}
