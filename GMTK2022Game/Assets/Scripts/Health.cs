using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

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

	public bool ignoreDamage;
	public DamageLayer DamageLayer => _damageLayer;

	private bool isHealthbarAttached;

	public void Start()
	{
		isHealthbarAttached = gameObject.GetComponentInChildren<Healthbar>() != null;

		if (_currentHealth > _currentMaxHealth)
			_currentHealth = _currentMaxHealth;
	}

	public void DealDamage(int amount, DamageLayer layer)
	{
        if (!ignoreDamage)
        {
			if (layer != _damageLayer)
				return;
			JSAM.AudioManager.PlaySound(Sounds.damage);
			if (amount > _currentMaxHealth)
				_currentHealth = 0;
			else
				_currentHealth -= amount;

			if (_currentHealth <= 0 && !isHealthbarAttached)
				Die();
			// else Die() in health bar
		}

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
