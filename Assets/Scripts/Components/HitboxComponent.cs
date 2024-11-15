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
        if (gameObject.GetComponent<InvincibilityComponent>().isInvincible)
            return;

        health?.Subtract(value);
        gameObject.GetComponent<InvincibilityComponent>().Flash();
    }

    public void Damage(Bullet bullet)
    {
        Damage(bullet.damage);
    }
}
