using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{

    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] private DamageLayer _damageLayer;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot() 
    {
        var projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);

        Vector2 dir = transform.position - transform.parent.position;
        dir.Normalize();

        var script = projectile.GetComponent<Projectile>();
        script.SetDamageLayer(_damageLayer);

        projectile.GetComponent<Rigidbody2D>().AddForce(dir * projectileSpeed, ForceMode2D.Force);
    }
}
