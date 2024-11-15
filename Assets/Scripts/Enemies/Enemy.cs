using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public int level;

    public IObjectPool<Enemy> objectPool;
    public Rigidbody2D rb;
    public FollowPlayer followPlayer;

    public Vector2 moveDirection;
    public float moveSpeed = 5.0f;


    [SerializeField]
    private float timeoutDelay = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        followPlayer = GetComponent<FollowPlayer>();
        FollowPlayer(false);
    }

    void Update()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    public void FollowPlayer(bool enable)
    {
        followPlayer.enabled = enable;
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
