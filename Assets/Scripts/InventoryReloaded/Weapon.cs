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

    public int price{get; set;}
    public int tier{get; set;}
    public float knockBack{get; set;}
    public float attackAngle{get; set;}
    public Weapon(int ID, string Name, int Damage, float Range, string ranged,string projectileName, float fireRate, int Price,int Tier,float KnockBack, float AttackAngle){
        id = ID;
        weaponName = Name;
        damage = Damage;
        range = Range;
        isRanged = ranged;
        projectilePrefabName = projectileName;
        shootInterval = fireRate;
        price = Price;
        tier = Tier;
        knockBack = KnockBack;
        attackAngle = AttackAngle;
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
