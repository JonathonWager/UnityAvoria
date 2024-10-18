using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int minJumps,maxJumps;
    public float jumpOdds = 1f;
    public float jumpOddsDivisor = 0.5f;
    public float maxSearchRadius = 6f;
    public float damage = 10f;

    public float speed = 7f;

    public GameObject jumpExplosion, endExplosion;
    

    int jumpCount = 0;
    string tagToFind = "enemy";

    Vector3 direction;

    List<GameObject> visited = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("enemy") || other.CompareTag("PlayerOnlyEnemy") ){
            if(other.gameObject.GetComponent<enemyStats>().hp > 0){
                visited.Add(other.gameObject);
                Debug.Log("Colliding");
                //dmg
                enemyStats eEnemy = other.GetComponent<enemyStats>();
                if (eEnemy != null)
                {
                    // Knockback direction and force
                    Vector2 knockbackDirection = direction;
                    eEnemy.takeDamage(damage, knockbackDirection, 0f);
                }
                 GameObject player = GameObject.FindGameObjectWithTag("character");
                if (player != null){
                    InventoryV4 iStats = player.GetComponentInChildren<InventoryV4>();
                    iStats.InvWeapons[1].CheckLevel();
                }
                //Figure out jump
                bool jump = false;
                Debug.Log(jumpCount + " < " + minJumps);
                if(jumpCount < minJumps){
                    jumpCount++;
                    jump = true;
                }else if(jumpCount < maxJumps){
                    if( Random.value < jumpOdds){
                        Debug.Log("RanDWON");
                        jumpOdds *= jumpOddsDivisor;
                        jumpCount++;
                        jump = true;
                    }
                }
                Debug.Log(jump + " is what");
                if(jump) {
                    GameObject closeEnemy = FindClosestWithTag();
                    if(closeEnemy != null){
                        Instantiate(jumpExplosion, other.transform.position, Quaternion.identity);
                        direction = (closeEnemy.transform.position - transform.position).normalized;
                        Debug.Log("should be shooting");
                        ShootObject(direction);
                        RotateObject(direction);
                    }else{
                        Debug.Log("ENEMY WAS NULl");
                        Instantiate(endExplosion, other.transform.position, Quaternion.identity);
                        Destroy(this.gameObject);
                    }
                }else{
                    Instantiate(endExplosion, other.transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
                
            }
    
        }
        if(other.CompareTag("wall")) {
            Instantiate(endExplosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

   public GameObject FindClosestWithTag()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, maxSearchRadius);
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(tagToFind))
            {
                float distance = (collider.transform.position - currentPosition).sqrMagnitude;
                if (distance < closestDistance && !visited.Contains(collider.gameObject))
                {
                    closestDistance = distance;
                    closest = collider.gameObject;
                }
            }
        }

        return closest;
    }
    void ShootObject(Vector3 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }
     void RotateObject(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Start()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;
        direction = (targetPosition - transform.position).normalized;
        ShootObject(direction);
        RotateObject(direction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
