using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int id;
    public string waeponName;

    public int damage;

    public float range;

    public Weapon(int ID, string Name, int Damage, float Range){
        id = ID;
        waeponName = Name;
        damage = Damage;
        range = Range;
    }

    public int getId(){
        return id;
    }
    public string getName(){
        return waeponName;
    }
    public float getRange(){
        return range;
    }
    public float getDamage(){
        return damage;
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
