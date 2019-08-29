using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyContainer;
    public GameObject[] enemyPrefabs; // container for 3 types of enemy
    public float spawnTime = 5f;
    public int numberOfSpawnedEnemiesPerWave = 3;

    private float currentSpawnTime;
    private float currentNumberOfSpawnedEnemies = 0;
    private bool isCanSpawn;

    private void Start()
    {
        currentSpawnTime = spawnTime;
        isCanSpawn = false;
    }

    private void Update()
    {
        SpawnTimerDecrease();
        //ReloadSpawnerByPressingKey(); // debug method
    }

    private void SpawnTimerDecrease()
    {
        if (!isCanSpawn) return;

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
        if (currentNumberOfSpawnedEnemies < numberOfSpawnedEnemiesPerWave)
        {
            GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject newEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity, enemyContainer);
            currentNumberOfSpawnedEnemies++;
        }
        else
        {
            isCanSpawn = false;
        }
    }

    // this method will called when player activate new enemies wave
    public void ReloadSpawner()
    {
        isCanSpawn = true;
        currentNumberOfSpawnedEnemies = 0;
    }

    // debug methods below
    private void ReloadSpawnerByPressingKey()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReloadSpawner();
        }
    }
}
