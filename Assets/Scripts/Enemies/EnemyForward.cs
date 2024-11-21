using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyForward : Enemy
{
    void Awake()
    {
        base.Awake();
        moveDirection = (transform.position.y > 0 ? -1 : 1) * Vector2.up;
    }

    void OnBecameInvisible()
    {
        moveDirection.y *= -1;
    }

    void Update()
    {
        rb.velocity = moveDirection.normalized * moveSpeed;

        transform.rotation = Quaternion.Euler(0, 0, 90 * moveDirection.x + 180 * moveDirection.y);
    }
}
