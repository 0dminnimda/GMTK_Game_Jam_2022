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

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public override void Action() 
    {
        var projectile = Instantiate(_projectilePrefab, transform.position, gameObject.transform.rotation);
        var script = projectile.GetComponent<Projectile>();
        script.SetDamageLayer(_damageLayer);
    }
}
