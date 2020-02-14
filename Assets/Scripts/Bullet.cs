using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    public int damage;

    public GameObject smallExplosion;

    //public GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster enemy = collision.GetComponent<Monster>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);

        Instantiate(smallExplosion, transform.position, Quaternion.identity);
    }
}
