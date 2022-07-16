using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private float _knockbackForce;

    private Rigidbody2D _parentRb;

    void Start()
    {
        _parentRb = GetComponentInParent<Rigidbody2D>();
    }

    
    void Update()
    {
        
    }

    public override void Action() 
    {
        if (Time.time < _nextActionTime)
            return;

        var projectile = Instantiate(_projectilePrefab, transform.position, gameObject.transform.rotation);
        _parentRb.AddForce(transform.right * -_knockbackForce);

        var script = projectile.GetComponent<Projectile>();
        script.SetDamageLayer(_damageLayer);

        _nextActionTime = Time.time + _actionCooldown;
    }
}
