using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public int level;

    public IObjectPool<Enemy> objectPool;
    public Rigidbody2D rb;

    [SerializeField]
    private float timeoutDelay = 30f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        objectPool.Release(this); 
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }
}
