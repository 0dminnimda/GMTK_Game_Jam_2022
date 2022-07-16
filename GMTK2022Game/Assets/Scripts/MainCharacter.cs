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

	void Update()
	{
		
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

			GameObject weaponObj = Instantiate(_weaponPrefabs[i], _pivotPoints[i].position, _pivotPoints[i].rotation, gameObject.transform);

			Weapon weaponComponent = weaponObj.GetComponent<Weapon>();

			_weapons[i] = weaponComponent;
			weaponComponent.SetDamageLayer(Assets.Scripts.Enums.DamageLayer.Enemy);

			FixedJoint2D fixedJointComp = weaponObj.gameObject.GetComponent<FixedJoint2D>();
			if (fixedJointComp != null)
			{
				fixedJointComp.connectedBody = _rb;
			}
		}
	}
}
