using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    private int NumberOfGeneratorActivated = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ActivateGenerator()
    {
        NumberOfGeneratorActivated++;
        CheckForAllGeneratorsActivated();
    }

    private void CheckForAllGeneratorsActivated()
    {
        if (NumberOfGeneratorActivated == 4)
        {
            print("ALL GENERATORS ACTIVE");
            EnemySpawnerParent.instance.DestroyAllEnemies();
        }
    }
}
