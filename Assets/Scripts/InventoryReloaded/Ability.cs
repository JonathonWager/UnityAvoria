using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public int id;

    public string name;

    public char type;
    // Start is called before the first frame update
    public Ability(int ID, string Name, char Type){
        id = ID;
        name = Name;
        type = Type;
    }
    public int getId(){
        return id;
    }
    public string getName(){
        return name;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
