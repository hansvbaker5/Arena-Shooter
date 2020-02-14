using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int health;

    public int damage;

    public GameObject bigExplosion;

    private AudioManager audioManager;

    bool dieOnce;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        audioManager.PlaySound("Monster Taking Damage");

        health -= damage;



        if (health <= 0 && !dieOnce)
        {
            dieOnce = true;
            Die();
        }
    } 

    private void Die()
    {
        Destroy(gameObject);

        Player player = GameObject.Find("Player(Clone)").GetComponent<Player>();
        if (player != null)
        {
            player.score += 1;
        }

        Instantiate(bigExplosion, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
