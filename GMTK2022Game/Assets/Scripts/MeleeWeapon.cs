using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class MeleeWeapon : Weapon
{
	[SerializeField] 
	int _damage = 1;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		var health = collision.gameObject.GetComponent<Health>();
		if(!health) {
			SoundMiss();
			return;
		}

		DealDamage(health);
	}

	private void DealDamage(Health health)
	{
		SoundDamage();
		health.DealDamage(_damage, _damageLayer);
	}

	private void SoundDamage() {
		JSAM.AudioManager.PlaySound(Sounds.sword_damage, transform);
	}

	private void SoundMiss() {
		JSAM.AudioManager.PlaySound(Sounds.sword_miss, transform);
	}
	public override void Action() { }

}
