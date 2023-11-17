using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int id;
    public string name;

    public string statusType;

    public float modifier;

    Potion(int ID, string Name, string statType, float Mod){
        id = ID;
        name = Name;
        statusType = statType;
        modifier = Mod;
    
    }

    public int getId(){
        return id;
    }
    public string getName(){
        return name;
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
