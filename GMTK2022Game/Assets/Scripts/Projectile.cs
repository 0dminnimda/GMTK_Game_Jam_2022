using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _lifetime = 0.10f;
    [SerializeField]
    private float _speed = 4f;

    void Awake()
    {
        Destroy(gameObject, _lifetime);
    }

    void Update()
	{
	}

}
