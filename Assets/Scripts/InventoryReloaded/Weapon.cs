using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon 
{

    public int id{ get; set; } 
    public string weaponName{ get; set; } 

    public int damage{ get; set; } 

    public float range{ get; set; } 
    public string isRanged{ get; set; } 

    public string projectilePrefabName{ get; set; } 
    public float shootInterval{get; set;}
     List<Weapon> allWeapons = new List<Weapon>();
    public Weapon(int ID, string Name, int Damage, float Range, string ranged,string projectileName, float fireRate){
        id = ID;
        weaponName = Name;
        damage = Damage;
        range = Range;
        isRanged = ranged;
        projectilePrefabName = projectileName;
        shootInterval = fireRate;
    }
     void makeWeapons(string[] weapons)
    {
        foreach (string w in weapons)
        {
            string[] atts = w.Split(',');

            // Create a new Weapon object and add it to the allWeapons list
            allWeapons.Add(new Weapon(int.Parse(atts[0]), atts[1], int.Parse(atts[2]), float.Parse(atts[3]), atts[4], atts[5], float.Parse(atts[6])));
        }
    }
    public Weapon findWeaponFromAll(int ID)
    {
        foreach (Weapon w in allWeapons)
        {
            if (w.id == ID)
            {
                return w;
            }
        }
        return null;
    }
    
    // Start is called before the first frame update
   

  
}
