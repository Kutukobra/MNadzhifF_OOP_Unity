using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100;
    public float health 
    {
        get;
        private set;
    }

    void Awake()
    {
        Refill();
    }

    public void Refill()
    {
        health = maxHealth;
    }
    
    public void Subtract(float value)
    {
        health -= value;

        //Debug.Log("New Health: " + health);
        if (health <= 0)
            Destroy(gameObject);
    }
}
