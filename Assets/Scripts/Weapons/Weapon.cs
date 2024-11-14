using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 1f;


    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer = 0;
    
    public Transform parentTransform;

    void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseFromPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.objectPool = objectPool;
        return bulletInstance;
    }

    void OnGetFromPool(Bullet pooledBullet)
    {
        pooledBullet.gameObject.SetActive(true);
    }

    void OnReleaseFromPool(Bullet pooledBullet)
    {
        pooledBullet.gameObject.SetActive(false);
    }

    void OnDestroyPooledObject(Bullet pooledBullet)
    {
        Destroy(pooledBullet.gameObject);
    }

    void FixedUpdate()
    {
        if (Time.time > timer)
        {
            Bullet bulletObject = objectPool.Get();

            if (bulletObject == null)
                return;

            bulletObject.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            
            bulletObject.Deactivate();  
            timer = Time.time + shootIntervalInSeconds;
        }
    }
}
