using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] 
    int _damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if(!health)
            return;

        DealDamage(health);
    }

    private void DealDamage(Health health)
    {
        health.DealDamage(_damage, _damageLayer);
    }

    public override void Action() { }

}
