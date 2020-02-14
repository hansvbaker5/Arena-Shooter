using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public CameraShake shake;

    private AudioManager audioManager;

    public bool equipped;

    public bool rapidFire;

    public bool scatterShot;

    private void Start()
    {
        shake = GameObject.Find("GameManager").GetComponent<CameraShake>();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (equipped)
        {
            if (timeBtwShots <= 0)
            {
                if (rapidFire)
                {
                    if (Input.GetButton("Fire1") && Time.time >= timeBtwShots)
                    {
                        Shoot();
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Shoot();
                    }
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        if (scatterShot)
        {
            GameObject currentPlayer = GameObject.FindGameObjectWithTag("Player");

            if (currentPlayer.GetComponent<PlayerMovement>().facingRight)
            {
                this.gameObject.transform.rotation *= Quaternion.Euler(Vector3.back * 15);
                SpawnScatterBulletLeft();
                SpawnScatterBulletLeft();
                SpawnScatterBulletLeft();
                SpawnScatterBulletLeft();
                SpawnScatterBulletLeft();
                SpawnScatterBulletLeft();
                this.gameObject.transform.rotation *= Quaternion.Euler(Vector3.forward * 345);
            }
            else
            {
                this.gameObject.transform.rotation *= Quaternion.Euler(Vector3.forward * 15);
                SpawnScatterBulletRight();
                SpawnScatterBulletRight();
                SpawnScatterBulletRight();
                SpawnScatterBulletRight();
                SpawnScatterBulletRight();
                SpawnScatterBulletRight();
                this.gameObject.transform.rotation *= Quaternion.Euler(Vector3.back * 345);
            }

            audioManager.PlaySound("Shoot");
            timeBtwShots = startTimeBtwShots;
            shake.Shake(0.05f, 0.1f);
        }
        else
        {
            audioManager.PlaySound("Shoot");
            Instantiate(projectile, shotPoint.position, transform.rotation);
            timeBtwShots = startTimeBtwShots;
            shake.Shake(0.05f, 0.1f);
        }

    }

    public void SpawnScatterBulletLeft()
    {
        Quaternion offset = new Quaternion(transform.rotation.x + Random.Range(-180, 180), transform.rotation.y, transform.rotation.z, transform.rotation.w);
        Instantiate(projectile, shotPoint.position, transform.rotation *= Quaternion.Euler(Vector3.forward * 5));
    }


    public void SpawnScatterBulletRight()
    {
        Quaternion offset = new Quaternion(transform.rotation.x + Random.Range(-80, 80), transform.rotation.y, transform.rotation.z, transform.rotation.w);
        Instantiate(projectile, shotPoint.position, transform.rotation *= Quaternion.Euler(Vector3.back * 5));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.transform.parent = collision.gameObject.GetComponent<Player>().gunSlot.transform;
            gameObject.transform.position = collision.gameObject.GetComponent<Player>().gunSlot.transform.position;
            gameObject.transform.rotation = collision.gameObject.GetComponent<Player>().gunSlot.transform.rotation;
            collision.gameObject.GetComponent<Player>().guns.Add(gameObject);
            equipped = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
