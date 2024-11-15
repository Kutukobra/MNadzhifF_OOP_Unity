using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform target;

    public float speed = 5f;
    public float rotateSpeed = 200f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = Player.Instance.transform;
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(transform.up, direction).z;

        rb.angularVelocity = rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }
}
