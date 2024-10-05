using System.Collections;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public GameObject explo;
    public float deleteTime = 10f;
    public float speed = 7f;
    public int dmg = 15;
    private Vector3 direction;
    private Vector3 startLocation;
    public float range;
     
    public float knockBack;

    public int collateralCount = 1;
    private int hitCount = 0;

    void Start()
    {
        startLocation = transform.position;
        Destroy(gameObject, deleteTime);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;
        direction = (targetPosition - transform.position).normalized;
        ShootObject(direction);
        RotateObject(direction);
    }

    void RotateObject(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void ShootObject(Vector3 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("boss") ){
             bossStats bEnemy = other.GetComponent<bossStats>();
            if (bEnemy != null)
            {
                // Knockback direction and force
                Vector2 knockbackDirection = direction;
                bEnemy.takeDamage(dmg, knockbackDirection, knockBack);
            }
            GameObject player = GameObject.FindGameObjectWithTag("character");
            if (player != null){
                InventoryV4 iStats = player.GetComponentInChildren<InventoryV4>();
                iStats.InvWeapons[1].CheckLevel();
            }
            hitCount += 1;
            if(hitCount >= collateralCount){
                Destroy(gameObject);
            }
            Instantiate(explo, other.transform.position, Quaternion.identity);
        }
        if (other.CompareTag("enemy"))
        {
            enemyStats eEnemy = other.GetComponent<enemyStats>();
            if (eEnemy != null)
            {
                // Knockback direction and force
                Vector2 knockbackDirection = direction;
                eEnemy.takeDamage(dmg, knockbackDirection, knockBack);
            }
            GameObject player = GameObject.FindGameObjectWithTag("character");
            if (player != null){
                InventoryV4 iStats = player.GetComponentInChildren<InventoryV4>();
                iStats.InvWeapons[1].CheckLevel();
            }
            hitCount += 1;
            if(hitCount >= collateralCount){
                Destroy(gameObject);
            }
            Instantiate(explo, other.transform.position, Quaternion.identity);
            
        }


    }

    void Update()
    {
        float distance = Vector3.Distance(startLocation, transform.position);
        if (distance > range)
        {
            Destroy(gameObject);
        }
    }
}
