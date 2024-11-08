using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    EdgeCollider2D edgeCollider;

    void Awake()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        CreateEdgeCollider();
    }

    void CreateEdgeCollider()
    {
        List<Vector2> edges = new List<Vector2>();

        edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));

        edgeCollider.SetPoints(edges);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.tag == "Portal")
        {
            Rigidbody2D collisionRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            collisionRigidBody.velocity = Vector2.Reflect(collisionRigidBody.velocity, -collision.GetContact(0).normal);
        }
    }
}
