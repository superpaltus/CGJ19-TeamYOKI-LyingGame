using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyContainer;
    public GameObject[] enemyPrefabs; // container for 3 types of enemy
    public float spawnTime = 5f;

    private float currentSpawnTime;

    private void Start()
    {
        currentSpawnTime = spawnTime;
    }

    private void Update()
    {
        SpawnTimerDecrease();
    }

    private void SpawnTimerDecrease()
    {
        if (enemyContainer.childCount >= 30) return;

        if (currentSpawnTime > 0f)
        {
            currentSpawnTime -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            currentSpawnTime = spawnTime;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject newEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity, enemyContainer);
    }
}
