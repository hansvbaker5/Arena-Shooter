using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public CameraShake shake;

    public int score;

    public Text scoreText;

    public GameObject deadSign;

    private MonsterSpawner spawner;

    private AudioManager audioManager;

    public GameObject gunSlot;

    public List<GameObject> guns;

    private void Start()
    {
        if (shake == null)
        {
            shake = GameObject.Find("GameManager").GetComponent<CameraShake>();
        }

        if (spawner == null)
        {
            spawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        }

        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }

        if (hearts[0] == null)
        {
            hearts[0] = GameObject.Find("Heart").GetComponent<Image>();
        }

        if (hearts[1] == null)
        {
            hearts[1] = GameObject.Find("Heart1").GetComponent<Image>();
        }

        if (hearts[2] == null)
        {
            hearts[2] = GameObject.Find("Heart2").GetComponent<Image>();
        }

        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        scoreText.text = score.ToString();

        spawner.spawnRate = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(transform.position.y <= -20)
        {
            TakeDamage(100);
        }

        if (guns.Count == 2)
        {
            Destroy(guns[0]);
        }
    }

    public void TakeDamage(int damage)
    {
        audioManager.PlaySound("Taking Damage");

        health -= damage;

        shake.Shake(0.2f, 0.2f);

        if (health <= 0)
        {
            Vector3 offset = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            shake.Shake(0.4f, 0.2f);
            Destroy(gameObject);
            Instantiate(deadSign, offset, Quaternion.identity);
        }
    }
}
