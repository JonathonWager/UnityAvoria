using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject player;
    public float shootInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("Shoot", 0f, shootInterval);
        player = GameObject.FindGameObjectWithTag("character");
    }

    void Shoot() {
        GameObject bullet1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // float randomAngle = Random.Range(0f, 360f);
        bullet1.transform.Rotate(Vector3.forward, 60f);

        GameObject bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet2.transform.Rotate(Vector3.forward, 120f);

        GameObject bullet3 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet3.transform.Rotate(Vector3.forward, 180f);

        GameObject bullet4 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet4.transform.Rotate(Vector3.forward, 240f);

        GameObject bullet5 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet5.transform.Rotate(Vector3.forward, 300f);

        GameObject bullet6 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet6.transform.Rotate(Vector3.forward, 360f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        characterStats cStats = player.GetComponent<characterStats>();
        cStats.takeDamage(damage);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
