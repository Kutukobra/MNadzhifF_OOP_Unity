using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public EnemySpawner enemySpawner;

    public int level;
    public Rigidbody2D rb;

    public Vector2 moveDirection = Vector2.zero;
    public float moveSpeed = 5.0f;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        enemySpawner?.OnEnemyKilled();
    }
}
