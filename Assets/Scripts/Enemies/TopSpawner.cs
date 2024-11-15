using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class TopSpawner : MonoBehaviour
{

    public Enemy enemy;
    private IObjectPool<Enemy> objectPool;

    private readonly bool collectionCheck = true;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer = 0;

    [Header("Spawning Timer")]
    public float period = 5f;
    public float timerOffset = 3f;

    [Header("Wave Size")]
    public int wave = 7;
    public int waveOffset = 4;
    public float spawnOffsetX = 100;

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
        pooledEnemy.followPlayer.enabled = false;
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

                if (enemy == null)
                    return;

                enemy.moveDirection = Vector2.down;

                Vector2 spawnPosition = Camera.main.ScreenToWorldPoint
                (
                new Vector2(
                    Random.Range(0, Screen.width),
                    Screen.height
                    )
                );

                enemy.transform.SetPositionAndRotation(
                    spawnPosition,
                    Quaternion.Euler(Vector3.down)
                    );
                
                enemy.Deactivate();    
            }
            timer = Time.time + period + Random.Range(-timerOffset, timerOffset);
        }
    }
}
