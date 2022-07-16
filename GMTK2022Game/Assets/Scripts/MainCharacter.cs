using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
	[SerializeField]
	private List<Weapon> _weapons;

	[SerializeField]
	private Rigidbody2D _rb;

	[SerializeField]
	private Transform[] _pivotPoints;

	public List<Weapon> WeaponList => _weapons;
	void Awake()
	{
		SpawnWeapons();
    }

	void Update()
	{
		
	}

	private void SpawnWeapons() 
	{
		for (int i = 0; i < _pivotPoints.Length; i++)
		{
			if (_weapons[i] == null)
				continue;

			Weapon weaponObj = Instantiate(_weapons[i], _pivotPoints[i].position, _pivotPoints[i].rotation, gameObject.transform);
			weaponObj.SetDamageLayer(Assets.Scripts.Enums.DamageLayer.Enemy);

			if (weaponObj.gameObject.GetComponent<FixedJoint2D>() != null)
			{
				weaponObj.gameObject.GetComponent<FixedJoint2D>().connectedBody = _rb;
			}
		}
	}
}
