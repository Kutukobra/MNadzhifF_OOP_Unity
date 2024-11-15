using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;

    [SerializeField]
    private float timeoutDelay = 10f;

    public IObjectPool<Bullet> objectPool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset Physics
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0; 

        objectPool.Release(this); 
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        StopAllCoroutines();
        objectPool.Release(this);
    }
}
