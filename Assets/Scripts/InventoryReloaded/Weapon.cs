using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int id{ get; set; } 
    public string weaponName{ get; set; } 

    public int damage{ get; set; } 

    public float range{ get; set; } 
    public string isRanged{ get; set; } 

    public string projectilePrefabName{ get; set; } 
    public Weapon(int ID, string Name, int Damage, float Range, string ranged,string projectileName){
        id = ID;
        weaponName = Name;
        damage = Damage;
        range = Range;
        isRanged = ranged;
        projectilePrefabName = projectileName;
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
