using Assets.Scripts.Enums;
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
	private int _damage = 1;

	private float _speed;

	private DamageLayer _damageLayer;
	private void Awake()
	{
		Destroy(gameObject, _lifetime);
	}

	private void Start()
	{
		_rb.AddForce(_speed * gameObject.transform.right);
	}

	public void SetDamageLayer(DamageLayer layer) => _damageLayer = layer;

	public void SetProjectileSpeed(float speed) => _speed = speed;

	public void OnCollisionEnter2D(Collision2D collision)
	{
		var health = collision.gameObject.GetComponent<Health>();
		if (health == null)
			SelfDestruct();
		else if (health.DamageLayer == _damageLayer)
		{
			DealDamage(health);
			SelfDestruct();
		}
		// else do nothig, no friendly fire ;P
	}

	private void SelfDestruct()
	{
		Destroy(gameObject);
	}

	private void DealDamage(Health health)
	{
		health.DealDamage(_damage, _damageLayer);
	}
}
