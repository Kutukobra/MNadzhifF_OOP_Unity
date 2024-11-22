using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{

    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 3;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public float spawnRadius = 10f;

    public bool isSpawning = false;

    void Start()
    {
        spawnCount = defaultSpawnCount;
        combatManager = transform.parent.gameObject.GetComponent<CombatManager>();
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;

        combatManager?.OnEnemyKilled();

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            spawnCount = defaultSpawnCount * (spawnCountMultiplier + multiplierIncreaseCount);
            multiplierIncreaseCount++;
        }
    }

    public void SpawnEnemies()
    {
        if (combatManager.waveNumber < spawnedEnemy.level)
            return;

        if (isSpawning)
            return;

        Debug.Log("Spawning " + spawnedEnemy);
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        isSpawning = true;
        for (int i = 0; i < spawnCount; i++)
        {
            Enemy enemy = Instantiate(spawnedEnemy, Random.onUnitSphere * spawnRadius, Quaternion.identity);
            enemy.enemySpawner = this;
            combatManager.totalEnemies++;

            yield return new WaitForSeconds(spawnInterval);
        }
        isSpawning = false;
    }

}
