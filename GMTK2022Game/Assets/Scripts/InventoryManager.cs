using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
	private GameObject[] _weaponPrefabs;

	private Weapon[] _weapons;

	[SerializeField]
	private GameObject _pickupPrefab;

	[SerializeField]
	private Rigidbody2D _rb;

	[SerializeField]
	private Transform[] _pivotPoints;

	[SerializeField]
	private Assets.Scripts.Enums.DamageLayer _damage_layer;

	public Weapon[] Items => _weapons;
	void Awake()
	{
		_weapons = new Weapon[4];
		SpawnWeapons();
    }

	public void SpawnWeapon(int index, GameObject weaponPrefab)
	{
		if (_weapons[index] != null)
		{
            GameObject pickupInstance = Instantiate(_pickupPrefab, _weapons[index].transform.position, Quaternion.identity);
			WeaponPickup pickupComponent = pickupInstance.GetComponent<WeaponPickup>();

			pickupComponent.SetWeaponPrefab(_weaponPrefabs[index]);
			pickupComponent.SetPickupSprite(_weapons[index].PickupSprite);
			pickupComponent.Push(_weapons[index].transform.right);

			Destroy(_weapons[index].gameObject);
		}

		GameObject weaponObj = Instantiate(weaponPrefab, _pivotPoints[index].position, _pivotPoints[index].rotation, gameObject.transform);

		Weapon weaponComponent = weaponObj.GetComponent<Weapon>();

		_weapons[index] = weaponComponent;
		_weaponPrefabs[index] = weaponPrefab;
		weaponComponent.SetDamageLayer(_damage_layer);

		FixedJoint2D fixedJointComp = weaponObj.gameObject.GetComponent<FixedJoint2D>();
		if (fixedJointComp != null)
		{
			fixedJointComp.connectedBody = _rb;
		}
	}

	private void SpawnWeapons()
	{
		for (int i = 0; i < _pivotPoints.Length; i++)
		{
			if (_weaponPrefabs[i] == null)
			{
				_weapons[i] = null;
				continue;
			}

			SpawnWeapon(i, _weaponPrefabs[i]);
		}
	}
}
