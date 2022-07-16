using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float _lifetime = 2f;
    [SerializeField]
    private float _speed = 200f;

    void Awake()
    {
        Debug.Log("Projectile created!", gameObject);
        Destroy(gameObject, _lifetime);
    }

    void Start()
	{
        _rb.AddForce(_speed * gameObject.transform.right);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision! " + gameObject.ToString());
    }
}
