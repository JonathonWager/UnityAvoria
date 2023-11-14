using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStats : MonoBehaviour
{
    public int hp;
    public float speed;

    public float dmg;

    public float def;

    public int getHp(){
        return hp;
    }
    public float getDmg(){
        return dmg;
    }
    public float getSpeed(){
        return speed;
    }
    public float getDef(){
        return def;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
