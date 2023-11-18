using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStats : MonoBehaviour
{
    public int hp;
    public float speed;

    public decimal dmg;
    public int dmg2;

    public float def;

    public float splashInterval;

    public float attackRate;

    public int getHp(){
        return hp;
    }
    public decimal getDmg(){
        return dmg;
    }
    public float getSpeed(){
        return speed;
    }
    public float getDef(){
        return def;
    }
    public float getSplashInt(){
        return splashInterval;
    }
    public float getAttackRate(){
        return attackRate;
    }
    // Start is called before the first frame update
    void Start()
    {
        dmg = (int)dmg2;
    }
    void checkHp(){
        if(hp <= 0 ){
            Destroy(this.gameObject);
        }
    }
    public void takeDamage(int damage){
        //Debug.Log("Taking Damage  "+ damage );
        if(hp - damage <= 0){
            Destroy(this.gameObject);
        }else{
            hp = hp - damage;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
