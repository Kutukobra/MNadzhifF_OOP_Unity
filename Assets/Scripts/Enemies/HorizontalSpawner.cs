using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class HorizontalSpawner : MonoBehaviour
{

    public Enemy enemy;
    private IObjectPool<Enemy> objectPool;

    public bool affectOrientation = true;

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
        timer = period;
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

            bool followsPlayer = Random.value > 0.5f;

            for (int i = 0; i < waveSize; i++)
            {
                Enemy enemy = objectPool.Get();

                int direction = Random.Range(0, 2) * 2 - 1;

                if (enemy == null)
                    return;
                            
                if (followsPlayer)
                {
                    enemy.FollowPlayer(true);
                }
                else
                {
                    enemy.moveDirection = Vector2.right * direction;
                }


                Vector2 spawnPosition = Camera.main.ScreenToWorldPoint
                (
                new Vector2(
                    direction > 0 ? Random.Range(-spawnOffsetX, 0) : Random.Range(Screen.width, Screen.width + spawnOffsetX),
                    Random.Range(0, Screen.height)
                    )
                );


                enemy.transform.SetPositionAndRotation(
                    spawnPosition,
                    affectOrientation ? Quaternion.Euler(0, 0, 90 * direction) : Quaternion.identity
                    );
                
                enemy.Deactivate();
            }

            timer = Time.time + period + Random.Range(-timerOffset, timerOffset);
        }
    }
}
