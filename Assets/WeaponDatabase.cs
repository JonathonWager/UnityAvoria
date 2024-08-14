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
            "1,Fists,2,10,M,NA,0",
            "2,Basic Sword,10,1.5,M,NA,0",
            "3,Great Sword,10,1.5,M,NA,2",
            "4,Long Sword,10,2.2,M,NA,0",
            "5,Basic Axe,10,2.2,M,NA,0",
            "6,Great Axe,10,2.2,M,NA,0",
            "7,Long Axe,10,2.2,M,NA,0",
            "8,BaseBall Bat,10,2.2,M,NA,0",
            "9,Chainsaw,10,2.2,M,NA,0",
            "10,Basic Bow,30,5,R,Arrow,1",
            "11,Long Bow,30,5,R,Arrow,1",
            "12,Great Bow,30,5,R,Arrow,1",
            "13,Compound Bow,30,5,R,Arrow,1",
            "14,Glock,30,5,R,Arrow,1",
            "15,ShotGun,5,5,R,ShotgunShell,1",
            "16,Ak-47,30,5,R,Bullet,1",
            "17,RPG,30,5,R,Arrow,1",
            "18,Boomerang,30,10,R,Boomerang,1",
            "19,Throwing Knives,10,7,R,ThrowingKnife,1"
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
                float.Parse(atts[6])
            );
            allWeapons.Add(weapon);
        }
    }

    public static Weapon GetWeaponById(int id)
    {
        return allWeapons.Find(w => w.id == id);
    }
}
