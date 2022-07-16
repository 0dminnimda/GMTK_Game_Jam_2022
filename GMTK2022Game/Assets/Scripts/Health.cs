using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField]
	private int _currentHealth;
	[SerializeField]
	private int _currentMaxHealth;

	public int CurrentHealth => _currentHealth;
	public int CurrentMaxHealth => _currentMaxHealth;

	public void Start()
	{
		if (_currentHealth > _currentMaxHealth)
			_currentHealth = _currentMaxHealth;
	}

	public void DealDamage(int amount)
	{
		if (amount > _currentMaxHealth)
			_currentMaxHealth = 0;
		else
			_currentMaxHealth -= amount;
	}

	public void Heal(int amount)
	{
		if (amount > _currentMaxHealth - _currentHealth)
			_currentHealth = _currentMaxHealth;
		else
			_currentHealth += amount;
	}
}
