using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class HandChaising : MonoBehaviour
{

    public float speed = 5f;
    public Transform player;
    public Animator animator;
    public int hitDelay = 2;
    //public Health health;
    public int damage = 1;
    private bool inTrigger;

    public Assets.Scripts.Enums.DamageLayer damageLayer;

    void Awake()
    {
        player = GameObject.Find("MainCharacter").GetComponent<Transform>();

    }
    // Start is called before the first frame update
    void Start()
    {
        Awake();
    }

    IEnumerator WaitForHit(Health health)
    {
        animator.SetTrigger("Smash");
        yield return new WaitForSeconds(hitDelay);
        print("BOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOM");
        if (inTrigger) DealDamage(health);
        yield break;
    }

    void DealDamage(Health health)
    {
        health.DealDamage(damage, damageLayer);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if (collision.gameObject.name == "MainCharacter")
        {
            inTrigger = true;
            print("Found the Player");
            StartCoroutine(WaitForHit(health));

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MainCharacter")
        {
            inTrigger = false;
        }
    }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
        }
    }
