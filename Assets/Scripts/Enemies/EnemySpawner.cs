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

    public bool isSpawning = false;


    void OnSpawnedDestroyed()
    {
        totalKill++;
        spawnCount--;

        if (spawnCount <= 0)
        {
            StartCoroutine("Spawn");
        }

        if (totalKill % minimumKillsToIncreaseSpawnCount == 0)
        {
            defaultSpawnCount += spawnCountMultiplier;
            spawnCountMultiplier += multiplierIncreaseCount;
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnInterval);

        spawnCount = defaultSpawnCount;
        for (int i = 0; i < defaultSpawnCount; i++)
        {
            Instantiate(spawnedEnemy, Random.Range(-10, 10) * Vector2.right + Vector2.up * 10, Quaternion.identity).OnDestroyed += OnSpawnedDestroyed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
