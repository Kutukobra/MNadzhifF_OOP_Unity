using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyHorizontal : Enemy
{
    void Awake()
    {
        base.Awake();
        moveDirection = (transform.position.x > 0 ? -1 : 1) * Vector2.right;
    }

    void OnBecameInvisible()
    {
        moveDirection.x *= -1;
    }

    void Update()
    {
        rb.velocity = moveDirection.normalized * moveSpeed;

        transform.rotation = Quaternion.Euler(0, 0, 90 * moveDirection.x + 180 * moveDirection.y);
    }
}
