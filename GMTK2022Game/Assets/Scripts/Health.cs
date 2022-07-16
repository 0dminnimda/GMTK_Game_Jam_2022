using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField]
	private int _currentHealth;
	[SerializeField]
	private int _currentMaxHealth;
	[SerializeField]
	private DamageLayer _damageLayer;

	public int CurrentHealth => _currentHealth;
	public int CurrentMaxHealth => _currentMaxHealth;
	public DamageLayer DamageLayer => _damageLayer;

	public void Start()
	{
		if (_currentHealth > _currentMaxHealth)
			_currentHealth = _currentMaxHealth;
	}

	public void DealDamage(int amount, DamageLayer layer)
	{
		if (layer != _damageLayer)
			return;
		if (amount > _currentMaxHealth)
			_currentHealth = 0;
		else
			_currentHealth -= amount;
		if (_currentHealth <= 0)
			Die();

	}

	public void Heal(int amount)
	{
		if (amount > _currentMaxHealth - _currentHealth)
			_currentHealth = _currentMaxHealth;
		else
			_currentHealth += amount;
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
