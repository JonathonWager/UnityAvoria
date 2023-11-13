using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public int hp = 100;
    public int speed = 5;
    public int def = 10;
    public int atk = 10;


    public void takeDamage(int damage){
        //hp = hp - (damage*(damage * (def  / 100)));
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
