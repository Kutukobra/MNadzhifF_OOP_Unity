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
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRigidBody.velocity = Vector2.Reflect(playerRigidBody.velocity, -collision.GetContact(0).normal);
        }
    }
}
