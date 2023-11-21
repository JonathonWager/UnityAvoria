using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAttcker : MonoBehaviour
{
    private GameObject player;
    public float speed = 10f;

    public int attackDamage = 20;
    public float attackRange = 3f;
    public float attackReset = 2f;

    public int spinCount = 3;
    public float spinSpeed = 360f;
    public float spinTime = 1f;
    private bool isSpinning = false;
    private bool isAttacking = false;
    public float engageRange = 1f;
    private float lastXRotation;
    public int getDamage(){
        return attackDamage;
    }
    public float getRange(){
        return attackRange;
    }
    // Start is called before the first frame update
    void Start()
    {
        
         player = GameObject.FindGameObjectWithTag("character");
         foreach (Transform child in transform)
                child.gameObject.SetActive(false);
    }
    void stopAttacking(){
        isAttacking = false;
    }
    void stopSpinning(){
        isSpinning = false;
          foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        Invoke("stopAttacking", attackReset);
    }
    void attack(){
        
         if (isSpinning)
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            Invoke("stopSpinning", spinTime);
            
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(isAttacking){
            attack();
        }else{
            if(Vector3.Distance(player.transform.position, transform.position) > engageRange){
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }else{
                isAttacking = true;
                isSpinning = true;
                lastXRotation = transform.rotation.eulerAngles.x;
            }
        }
        
        
    }
}
