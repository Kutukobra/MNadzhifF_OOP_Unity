using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int totalEnemies = 0;

    [Header("UI Data")]
    public int points;
    public int waveNumber = 0;
    public int enemyRemaining = 0;

    bool isSpawning = false;

    void Update()
    {
        UI_Data.UpdateGameUI(
            Player.Instance.GetComponent<HealthComponent>().health,
            points,
            waveNumber,
            enemyRemaining
        );

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

        enemyRemaining = 0;
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.SpawnEnemies();
            enemyRemaining += enemySpawner.spawnCount;
        }
        isSpawning = false;
    }

    public void OnEnemyKilled()
    {
        totalEnemies--;
    }

    public void OnEnemyKilled(int point)
    {
        OnEnemyKilled();
        points += point;
    }
}
