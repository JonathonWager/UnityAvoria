using System.Collections;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public GameObject explo;
    public float deleteTime = 10f;
    public float speed = 7f;
    private int dmg = 15;
    private Vector3 direction;
    private Vector3 startLocation;
    private float range;

    void Start()
    {
        startLocation = transform.position;
        
        dmg = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().adjAtk;
        range = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().range;

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
        if (other.CompareTag("enemy"))
        {
            enemyStats eEnemy = other.GetComponent<enemyStats>();
            if (eEnemy != null)
            {
                // Knockback direction and force
                Vector2 knockbackDirection = direction;
                float knockbackForce = 5f;

                eEnemy.takeDamage(dmg, knockbackDirection, knockbackForce);
            }

            Instantiate(explo, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.CompareTag("Untagged"))
        {
            Destroy(gameObject);
            Instantiate(explo, transform.position, Quaternion.identity);
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
