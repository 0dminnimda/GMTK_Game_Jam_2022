using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField]
	protected DamageLayer _damageLayer;
	public void SetDamageLayer(DamageLayer layer) => _damageLayer = layer;

	[SerializeField]
	protected float _actionCooldown;

	protected float _nextActionTime = 0f;

	public abstract void Action();

	[SerializeField]
	protected Sprite _pickupSprite;
	public Sprite PickupSprite => _pickupSprite;
}
