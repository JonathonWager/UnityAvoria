using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStats : MonoBehaviour
{
    public int hp;
    public float speed;

    public decimal dmg;
    public int dmg2;

    public float def;

    public float splashInterval;

    public float attackRate;
    private Animator animator;
    public float AgroRange;
    public bool isAgro;
    private GameObject player;
    
    void stopDamageAnimation(){
        animator.SetBool("isHurt", false);
    }
   
    // Start is called before the first frame update
    void Start()
    {
        dmg = (int)dmg2;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("character");
    }
    void checkHp(){
        if(hp <= 0 ){
            Destroy(this.gameObject);
        }
    }
    public void takeDamage(float damage){
        Debug.Log("Taking Damage  "+ damage );
        if(hp - damage <= 0){
            animator.SetBool("isDead", true);
            
           
        }else{
            hp = (int)(hp - damage);
            animator.SetBool("isHurt", true);
        }
    }
    void killEnemey(){
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < AgroRange){
            isAgro = true;
        }
    }
}
