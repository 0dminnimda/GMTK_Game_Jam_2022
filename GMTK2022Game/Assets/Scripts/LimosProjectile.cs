using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimosProjectile : MonoBehaviour
{
    public int damage;
    public bool playerBullet;
    public float baseSpeed;
    public float missChance;
    public float deleteIn;
    private float timer;
    public bool destroyOnHit;
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * baseSpeed);
        if(timer > deleteIn)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            if(Random.Range(0f, 1f) >= missChance)
            {
                collision.gameObject.GetComponent<Health>().DealDamage(damage, playerBullet ? Assets.Scripts.Enums.DamageLayer.Enemy : Assets.Scripts.Enums.DamageLayer.Player);
                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }

    }
}
