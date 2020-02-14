using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monster;

    public float spawnRate;
    private float btwSpawnRate;

    private int monsterID;

    public Vector3 spawnLocation;

    private int typeOfSpawn;

    private void Start()
    {
        btwSpawnRate = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        typeOfSpawn = Random.Range(0, 2);

        monsterID = Random.Range(0, monster.Length);
        spawnLocation.x = Random.Range(-2f, 2f);
        spawnLocation.y = transform.position.y;

        if (btwSpawnRate <= 0)
        {
            if (typeOfSpawn == 0)
            {
                spawnRate -= 0.05f;
                Instantiate(monster[monsterID], spawnLocation, transform.rotation);
                btwSpawnRate = spawnRate;
            }
            else if (typeOfSpawn == 1)
            {
                StartCoroutine(SpawnThreeMonsters(monsterID));
                btwSpawnRate = spawnRate;
            }
        }
        else
        {
            btwSpawnRate -= Time.deltaTime;
        }
    }

    IEnumerator SpawnThreeMonsters(int id)
    {
        Instantiate(monster[id], spawnLocation, transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(monster[id], spawnLocation, transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(monster[id], spawnLocation, transform.rotation);
        yield return new WaitForSeconds(0.5f);
    }
}
