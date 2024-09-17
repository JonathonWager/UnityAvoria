using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    public static List<Weapon> allWeapons = new List<Weapon>();
    //For Reference
    //  public int id{ get; set; } 
    // public string weaponName{ get; set; } 

    // public int damage{ get; set; } 

    // public float range{ get; set; } 
    // public string isRanged{ get; set; } 

    // public string projectilePrefabName{ get; set; } 
    // public float shootInterval{get; set;}
    // List<Weapon> allWeapons = new List<Weapon>();

    // public int price{get; set;}
    // public int tier{get; set;}
    // public float knockBack{get; set;}
    // public float attackAngle{get; set;}
    void Awake()
    {
        // Initialize the database with all weapons
        string[] weapons = {
            "1,Fists,5,1.2,M,NA,0,0,0,2,100",
            "2,Basic Sword,10,1.5,M,NA,0,10,1,2.2,100",
            "3,Great Sword,15,1.7,M,NA,2,20,2,2.5,100",
            "4,Long Sword,25,2.2,M,NA,0,30,3,2.7,100",
            "5,Basic Axe,15,1.5,M,NA,0,15,1,1,100",
            "6,Great Axe,20,2.2,M,NA,0,25,2,1,100",
            "7,Long Axe,27,2.5,M,NA,0,45,4,1,100",
            "8,BaseBall Bat,8,1.5,M,NA,0,5,1,1,100",
            "9,Chainsaw,25,1.5,M,NA,0,55,4,1,100",
            "10,Basic Bow,5,5,R,Arrow,1,0,0,1,0",
            "11,Long Bow,12,5,R,Arrow,1,25,2,1,0",
            "12,Great Bow,22,5,R,Arrow,1,35,3,1,0",
            "13,Compound Bow,30,5,R,Arrow,1,50,4,1,0",
            "14,Glock,30,5,R,Bullet,1,20,2,1,0",
            "15,ShotGun,5,5,R,ShotgunShell,1,45,3,1,0",
            "16,Ak-47,30,5,R,Bullet,1,60,4,1,0",
            "17,RPG,30,5,R,Arrow,1,100,5,1,0",
            "18,Boomerang,30,10,R,Boomerang,1,40,3,1,0",
            "19,Throwing Knives,10,7,R,ThrowingKnife,1,15,1,1,0"
        };
        makeWeapons(weapons);
    }

    void makeWeapons(string[] weapons)
    {
        foreach (string w in weapons)
        {
            string[] atts = w.Split(',');
            Weapon weapon = new Weapon(
                int.Parse(atts[0]),
                atts[1],
                int.Parse(atts[2]),
                float.Parse(atts[3]),
                atts[4],
                atts[5],
                float.Parse(atts[6]),
                int.Parse(atts[7]),
                int.Parse(atts[8]),
                float.Parse(atts[9]),
                float.Parse(atts[10])
            );
            allWeapons.Add(weapon);
        }
    }

    public static Weapon GetWeaponById(int id)
    {
        return allWeapons.Find(w => w.id == id);
    }
    public static int WeaponCount()
    {
        return allWeapons.Count;
    }
    public static List<Weapon> getAllWeapons(){
        return allWeapons;
    }
}
