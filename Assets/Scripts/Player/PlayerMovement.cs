using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    Vector2 inputDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    public float rotateSpeed = 0.1f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity =  2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);

    }

    public void Move()
    {
        inputDirection = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;

        rb.AddForce(inputDirection * moveVelocity);

        //rb.angularVelocity = (-inputDirection.x * rotateSpeed);

        rb.AddForce(rb.velocity.normalized * GetFriction() * Time.fixedDeltaTime);
        
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
        );

        if (!IsMoving() && 
            Mathf.Abs(rb.velocity.x) <= stopClamp.x && 
            Mathf.Abs(rb.velocity.y) <= stopClamp.y
            )
        {
            rb.velocity = Vector2.zero;
        }
    }

    public Vector2 GetFriction()
    {
        return new Vector2(inputDirection.x == 0 ? stopFriction.x : moveFriction.x, inputDirection.y == 0 ? stopFriction.y : moveFriction.y);
    }
    
    public void MoveBound()
    {

    }

    public bool IsMoving()
    {
        return inputDirection != Vector2.zero;
    }
}
