using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RangedWeapon : Weapon
{
	[SerializeField]
	private GameObject _projectilePrefab;

	[SerializeField]
	private float _projectileSpeed;

	[SerializeField]
	private float _knockbackForce;

	[SerializeField]
	private float _spray;

	private Rigidbody2D _parentRb;

	void Awake()
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

		var projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.Euler(0f, 0f, gameObject.transform.rotation.eulerAngles.z + Random.Range(-_spray, _spray)));
		_parentRb.AddForce(transform.right * -_knockbackForce);

		var script = projectile.GetComponent<Projectile>();
		script.SetDamageLayer(_damageLayer);
		script.SetProjectileSpeed(_projectileSpeed);

		_nextActionTime = Time.time + _actionCooldown;
	}
}
