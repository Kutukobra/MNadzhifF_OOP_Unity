using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 0;
    public int totalEnemies = 0;

    bool isSpawning = false;

    void Update()
    {
        if (totalEnemies <= 0 && !isSpawning)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;
        yield return new WaitForSeconds(waveInterval);
                
        waveNumber++;
        Debug.Log("Level " + waveNumber);

        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.SpawnEnemies();
        }
        isSpawning = false;
    }

    public void OnEnemyKilled()
    {
        totalEnemies--;
    }
}
