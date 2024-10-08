using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawBlade : MonoBehaviour
{
    public bool toEnd = true;
    public bool toStart = false;
    public Vector3 startLocation;
    public Vector3 endLocation;
    public float speed = 5f;
    public int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("character"))
        {
            other.GetComponent<characterStats>().takeDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (toEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endLocation, speed * Time.deltaTime);
        }
        if (toStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, startLocation, speed * Time.deltaTime);
        }

        if (transform.position == startLocation)
        {
            toEnd = true;
            toStart = false;
            Flip();
        }
        else if (transform.position == endLocation)
        {
            toEnd = false;
            toStart = true;
            Flip();
        }
    }

    private void Flip()
    {
        // Flip the GameObject and all child objects by inverting the x-scale
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        // If there's a Particle System, flip its scale as well
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null)
        {
            // Flip the Particle System scale
            Vector3 particleScale = particleSystem.transform.localScale;
            particleScale.x *= -1;
            particleSystem.transform.localScale = particleScale;
        }
    }
}
