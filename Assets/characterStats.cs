using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public decimal hp = 100;
    public int speed = 5;
    public decimal def = 10;
    public decimal atk = 10;


    public void takeDamage(decimal damage){
        Debug.Log(hp);
        hp = hp - (damage * (1-(def  / 100)));
   
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
