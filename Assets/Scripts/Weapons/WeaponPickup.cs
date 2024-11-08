using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    [SerializeField] Weapon weaponHolder;

    Weapon weapon;

    void Awake()
    {
        if (weaponHolder != null)
            weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        TurnVisual(false, weapon);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            GameObject player = other.gameObject;

            if (player.GetComponentInChildren<Weapon>() != null)
            {
                foreach(var weapon in player.GetComponentsInChildren<Weapon>())
                {
                    TurnVisual(false, weapon);
                }
            }

            weapon.parentTransform = player.transform;
            weapon.transform.SetParent(weapon.parentTransform, false);
            TurnVisual(true, weapon);
        }
    }

    void TurnVisual(bool on)
    {
        TurnVisual(on, this.weapon);
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        weapon.enabled = on;
        weapon.GetComponent<SpriteRenderer>().enabled = on;
        weapon.GetComponent<Animator>().enabled = on;
    }
}
