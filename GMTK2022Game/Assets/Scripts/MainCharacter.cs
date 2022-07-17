using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
	[SerializeField]
	private GameObject[] _weaponPrefabs;

	private Weapon[] _weapons;

	[SerializeField]
	private Rigidbody2D _rb;

	[SerializeField]
	private Transform[] _pivotPoints;

	public Weapon[] WeaponList => _weapons;
	void Awake()
	{
		_weapons = new Weapon[4];
		SpawnWeapons();
    }

	public void SpawnWeapon(int index, GameObject weaponPrefab) 
	{
		if (_weapons[index] != null)
			Destroy(_weapons[index].gameObject);

		GameObject weaponObj = Instantiate(weaponPrefab, _pivotPoints[index].position, _pivotPoints[index].rotation, gameObject.transform);

		Weapon weaponComponent = weaponObj.GetComponent<Weapon>();

		_weapons[index] = weaponComponent;
		weaponComponent.SetDamageLayer(Assets.Scripts.Enums.DamageLayer.Enemy);

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
