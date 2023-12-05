using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossStats : MonoBehaviour
{
    public int hp = 1000;
    private int halfhp;
    private int quarterhp;
    public string bossType;
    private bool canBeDamaged = true;
    public void takeDamage(int damage)
    {
        // Apply damage after considering defense
        if(canBeDamaged){
            hp = hp - damage;
        }

    }
    void beegBossUpdate(){
        beegBoss bStats = gameObject.GetComponent<beegBoss>();
        if(hp <= halfhp){
            bStats.pastHalf = true;
        }
        if(hp <= quarterhp){
            //bStats.pastHalf = true;
        }
        if(bStats.isVulnerable){
            canBeDamaged = true;
        }else{
            canBeDamaged = false;
        }
    }
    void Update(){
        if(bossType == "beegBoss"){
            beegBossUpdate();
        }
        if(hp <= 0){
            Destroy(gameObject);
        }
    }
    void Start(){
        halfhp = hp /2;
        quarterhp = hp /4;
    }
}
