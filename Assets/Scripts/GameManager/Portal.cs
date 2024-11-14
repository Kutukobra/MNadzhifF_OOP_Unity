using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    Vector2 newPosition;

    private float deltaMovement = 1;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }

    void Update()
    {
        if (Player.Instance.GetComponentInChildren<Weapon>() != null)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<Animator>().enabled = true;
        }

        if (Vector2.Distance(newPosition, transform.position) < 0.5)
        {
            ChangePosition();
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, newPosition, 0.1f * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.LevelManager.LoadScene("Main");
    }

    void ChangePosition()
    {
        newPosition = transform.position + new Vector3(Random.Range(-deltaMovement, deltaMovement), Random.Range(-deltaMovement, deltaMovement), 0.0f);
    }
}
