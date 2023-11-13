using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexEnemy : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float shootInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("Shoot", 0f, shootInterval);

    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        float randomAngle = Random.Range(0f, 360f);
        bullet.transform.Rotate(Vector3.forward, randomAngle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
