using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    [SerializeField] int _damage = 1;
    [SerializeField] private DamageLayer _damageLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Health>())
        {
            return;
        }

        var health = collision.GetComponent<Health>();

        DealDamage(health);
    }

    private void DealDamage(Health health)
    {
        health.DealDamage(_damage, _damageLayer);
    }

}
