using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float _enemySpeed = 0.75f;

	[SerializeField]
	private float _rotationSpeed = 2f;

	[SerializeField]
	private GameObject[] _weaponPrefabs;

	private Weapon[] _weapons;

	[SerializeField]
	private Rigidbody2D _rb;

	[SerializeField]
	private Transform[] _pivotPoints;

	private GameObject _target;
	private Vector3 _targetPos;

	// Start is called before the first frame update
	void Awake()
	{
		_weapons = new Weapon[4];
		_target = FindObjectOfType<MainCharacter>().gameObject;
		SpawnWeapons();
	}

	void Start()
	{
		StartCoroutine(nameof(DoCheck));
	}

	// Update is called once per frame
	void Update()
	{
		if(_target != null)
		{
			_targetPos = _target.transform.position;
			RotateTowardsTarget();
			float step = _enemySpeed * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, _targetPos, step);
		}
	}

	private void RotateTowardsTarget()
	{
		Vector2 vectorToTarget = _targetPos - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotationSpeed);
	}

	IEnumerator DoCheck()
	{
		for (; ;)
		{
			foreach (Weapon wep in _weapons)
			{
				if (wep != null)
					wep.Action();
			}
			yield return new WaitForSeconds(1f);
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

			GameObject weaponObj = Instantiate(_weaponPrefabs[i], _pivotPoints[i].position, _pivotPoints[i].rotation, gameObject.transform);

			Weapon weaponComponent = weaponObj.GetComponent<Weapon>();
			
			_weapons[i] = weaponComponent;
			weaponComponent.SetDamageLayer(Assets.Scripts.Enums.DamageLayer.Player);

			FixedJoint2D fixedJointComp = weaponObj.gameObject.GetComponent<FixedJoint2D>();
			if (fixedJointComp != null)
			{
				fixedJointComp.connectedBody = _rb;
			}
		}
	}
}
