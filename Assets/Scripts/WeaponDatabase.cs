using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    public static List<Weapon> allWeapons = new List<Weapon>();
    
    void Awake()
    {
        // Initialize the database with all weapons
        string[] weapons = {
            "1,Fists,2,1,M,NA,0,0,0",
            "2,Basic Sword,10,1.5,M,NA,0,10,1",
            "3,Great Sword,10,1.5,M,NA,2,20,2",
            "4,Long Sword,10,2.2,M,NA,0,30,3",
            "5,Basic Axe,10,2.2,M,NA,0,15,1",
            "6,Great Axe,10,2.2,M,NA,0,25,2",
            "7,Long Axe,10,2.2,M,NA,0,45,4",
            "8,BaseBall Bat,10,2.2,M,NA,0,5,1",
            "9,Chainsaw,10,2.2,M,NA,0,55,4",
            "10,Basic Bow,10,5,R,Arrow,1,0,0",
            "11,Long Bow,30,5,R,Arrow,1,25,2",
            "12,Great Bow,30,5,R,Arrow,1,35,3",
            "13,Compound Bow,30,5,R,Arrow,1,50,4",
            "14,Glock,30,5,R,Arrow,1,20,2",
            "15,ShotGun,5,5,R,ShotgunShell,1,45,3",
            "16,Ak-47,30,5,R,Bullet,1,60,4",
            "17,RPG,30,5,R,Arrow,1,100,5",
            "18,Boomerang,30,10,R,Boomerang,1,40,3",
            "19,Throwing Knives,10,7,R,ThrowingKnife,1,15,1"
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
                int.Parse(atts[8])
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
