using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetting : Enemy
{

    public Transform target;
    public float rotateSpeed = 30f;

    void Awake()
    {
        target = Player.Instance.transform;
        base.Awake();
    }

    void Start()
    {
        target = Player.Instance.transform;
        base.Awake();
    }

    void Update()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(transform.up, direction).z;

        rb.angularVelocity = rotateAmount * rotateSpeed;
        rb.velocity = transform.up * moveSpeed;
    }
}
