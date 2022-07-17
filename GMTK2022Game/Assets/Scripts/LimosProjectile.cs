using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimosProjectile : MonoBehaviour
{
    public int damage;
    public bool playerBullet;
    public float baseSpeed;
    public float missChance;
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * baseSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            if(Random.Range(0f, 1f) >= missChance)
                collision.gameObject.GetComponent<Health>().DealDamage(damage, playerBullet ? Assets.Scripts.Enums.DamageLayer.Enemy : Assets.Scripts.Enums.DamageLayer.Player);
        }

    }
}
