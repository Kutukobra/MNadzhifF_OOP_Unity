using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public int level;

    public delegate void OnDestroyedHandler();
    public event OnDestroyedHandler OnDestroyed;

    void OnDisable()
    {
        if(OnDestroyed != null)
            OnDestroyed();
    }

    public IObjectPool<Enemy> objectPool;
    public Rigidbody2D rb;

    public Vector2 moveDirection = Vector2.zero;
    public float moveSpeed = 5.0f;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
