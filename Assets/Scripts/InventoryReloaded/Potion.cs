using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int id;
    public string potionName{get; set;}

    public string statusType;

    public float modifier;
    public bool isSpecial;

    public Potion(int ID, string Name, string statType, float Mod, bool special){
        id = ID;
        potionName = Name;
        statusType = statType;
        modifier = Mod;
        isSpecial = special;
    
    }

    public int getId(){
        return id;
    }
    public string getName(){
        return potionName;
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
