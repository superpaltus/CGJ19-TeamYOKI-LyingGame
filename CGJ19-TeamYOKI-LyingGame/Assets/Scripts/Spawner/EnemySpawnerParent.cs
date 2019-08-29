using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerParent : MonoBehaviour
{
    public static EnemySpawnerParent instance;

    public Transform enemyContainer;
    public bool canSpawn = true;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void DestroyAllEnemies()
    {
        canSpawn = false;

        for (int i = 0; i < enemyContainer.childCount; i++)
        {
            print("destroying");
            Destroy(enemyContainer.GetChild(i).gameObject);
        }
    }
}
