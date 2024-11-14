using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{

    public Enemy enemy;
    private IObjectPool<Enemy> objectPool;

    [SerializeField]
    public float speed = 5f;

    private readonly bool collectionCheck = true;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer = 0;

    [Header("Spawning Timer")]
    public float period = 7f;
    public float timerOffset = 5f;

    [Header("Wave Size")]
    public int wave = 7;
    public int waveOffset = 3;

    void Awake()
    {
        objectPool = new ObjectPool<Enemy>(CreateEnemy, OnGetFromPool, OnReleaseFromPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    Enemy CreateEnemy()
    {
        Enemy enemyInstance = Instantiate(enemy);
        enemyInstance.objectPool = objectPool;
        return enemyInstance;
    }

    void OnGetFromPool(Enemy pooledEnemy)
    {
        pooledEnemy.gameObject.SetActive(true);
    }

    void OnReleaseFromPool(Enemy pooledEnemy)
    {
        pooledEnemy.gameObject.SetActive(false);
    }

    void OnDestroyPooledObject(Enemy pooledEnemy)
    {
        Destroy(pooledEnemy.gameObject);
    }

    void Update()
    {
        if (Time.time > timer)
        {
            int waveSize = wave + Random.Range(-waveOffset, waveOffset);

            for (int i = 0; i < waveSize; i++)
            {
                Enemy enemy = objectPool.Get();

                int direction = Random.Range(0, 2) * 2 - 1;

                if (enemy == null)
                    return;

                enemy.rb.velocity = Vector2.right * direction * speed;

                Vector2 spawnPosition = Camera.main.ScreenToWorldPoint
                (
                new Vector2(
                    direction > 0 ? 0 : Screen.width,
                    Random.Range(0, Screen.height)
                    )
                );

                enemy.transform.SetPositionAndRotation(
                    spawnPosition,
                    Quaternion.Euler(0, 0, 90 * direction)
                    );

                timer = Time.time + period + Random.Range(-timerOffset, timerOffset);
            }
        }
    }
}
