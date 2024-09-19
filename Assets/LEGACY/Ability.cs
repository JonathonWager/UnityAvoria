using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public int id;

    public string abilityName;

    public char type;
    // Start is called before the first frame update
    public Ability(int ID, string Name, char Type){
        id = ID;
        abilityName = Name;
        type = Type;
    }
    public int getId(){
        return id;
    }
    public string getName(){
        return abilityName;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
