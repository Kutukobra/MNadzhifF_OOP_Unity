using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    HealthComponent health;

    public void Awake()
    {
        health = gameObject.GetComponent<HealthComponent>();
        if (health == null)
            Debug.Log("Cant get HealthComponent");
    }

    public void Damage(float value)
    {
        health?.Subtract(value);
    }

    public void Damage(Bullet bullet)
    {
        Damage(bullet.damage);
    }
}
