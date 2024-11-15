using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;
    public float damage = 0;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(this.tag))
            return;

        other.gameObject.GetComponent<HitboxComponent>()?.Damage(damage);

        other.gameObject.GetComponent<InvincibilityComponent>()?.Flash();
    }
}
