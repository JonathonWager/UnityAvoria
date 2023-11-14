using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weapon : MonoBehaviour
{
    int id;
    string weaponType;
    string weaponName;
    int weaponDamage;
    float weaponRange;

    public weapon(int ID, string Type, string Name, int Damage, float Range){
        this.id = ID;
        this.weaponType = Type;
        this.weaponName = Name;
        this.weaponRange = Range;
        this.weaponDamage = Damage;
    }
    public int getId(){
        return id;
    }
    string getType(){
        return weaponType;
    }

    public string getName(){
        return weaponName;
    }

    public int getDamage(){
        return weaponDamage;
    }
    public float getRange(){
        return weaponRange;
    }
    void start()
    {
        string[] weapons = {
            "Melee,Great Sword,10,2"
        };
        
    }
}
