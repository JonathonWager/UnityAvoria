using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyStats : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    public void takeDamage(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
         
        hp -= (int)damage;
        // Apply knockback
  

            
        
    }
}
