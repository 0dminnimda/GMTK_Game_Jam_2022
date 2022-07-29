using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChaising : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;
    public float preHitDelay = 0.42f;
    public float fullHitDelay = 1f;

    public Animator animator;

    public GameObject player;
    private Health playerHealth;

    private bool inTrigger = false;
    private bool isAttacking = false;

    public Assets.Scripts.Enums.DamageLayer damageLayer;

    void Start()
    {
        playerHealth = player.GetComponent<Health>();
    }

    void Update()
    {
        if (player == null)
            return;

        transform.position += (Vector3)Vector2.MoveTowards(Vector2.zero, player.transform.position - transform.position, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTarget(collision))
        {
            inTrigger = true;
            Hit();
        }
        // var health = collision.gameObject.GetComponent<Health>();

        // if (health != null && health.DamageLayer == damageLayer)
        // {
        //     inTrigger = true;
        //     StartCoroutine(WaitForHit(health));
        // }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsTarget(collision))
            Hit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsTarget(collision))
            inTrigger = false;
    }

    bool IsTarget(Collider2D collision) => GameObject.ReferenceEquals(collision.gameObject, player);

    void Hit()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(WaitForHit(playerHealth));
        }
    }

    IEnumerator WaitForHit(Health health)
    {
        animator.Play("Smash");
        yield return new WaitForSeconds(preHitDelay);
        if (inTrigger) DealDamage(health);
        yield return new WaitForSeconds(fullHitDelay - preHitDelay);
        isAttacking = false;
    }

    void DealDamage(Health health)
    {
        health.DealDamage(damage, damageLayer);
    }

}
