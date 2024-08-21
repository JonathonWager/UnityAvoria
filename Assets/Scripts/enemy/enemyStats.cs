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
    public int minGold = 0;
    public int maxGold = 2;
    void stopDamageAnimation(){
        animator.SetBool("isHurt", false);
    }
   void DisableAllOtherScripts()
    {
        // Get all MonoBehaviour scripts attached to this GameObject
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        // Loop through each script and disable it if it's not this one
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)  // Do not disable this script
            {
                script.enabled = false;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        dmg = (int)dmg2;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("character");
    }

    public void takeDamage(float damage){
        if(hp - damage <= 0){
            animator.SetBool("isDead", true);
            
           
        }else{
            hp = (int)(hp - damage);
            animator.SetBool("isHurt", true);
        }
    }
    void killEnemey(){
        SpawnGold();
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(hp <= 0){
            DisableAllOtherScripts();
        }
        if(Vector3.Distance(transform.position, player.transform.position) < AgroRange){
            isAgro = true;
        }
    }
    void SpawnGold()
    {
        
        int goldAmount = Random.Range(minGold, maxGold);
        if(goldAmount > 0){
            Debug.Log("Spawnig GOld");
            GameObject goldPrefab = Resources.Load<GameObject>("Gold");
            GameObject goldObject = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                // Get the Gold component attached to the instantiated GameObject
            Gold goldScript = goldObject.GetComponent<Gold>();

            // Now call the Initialize method on the Gold component
            
            goldScript.Initialize(goldAmount);
        }
       
    }
}
