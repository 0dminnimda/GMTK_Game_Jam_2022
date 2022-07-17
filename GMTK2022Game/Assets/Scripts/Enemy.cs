using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float _enemySpeed = 0.75f;

	[SerializeField]
	private float _rotationSpeed = 2f;

	private InventoryManager _inventory_manager;

	[SerializeField]
    private Pathfinding.AIPath _aIPath;

	[SerializeField]
    private GameObject _target;
    private Vector3 _targetPos;

	void Awake()
	{
		_inventory_manager = GetComponent<InventoryManager>();
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
	        if (_aIPath.reachedDestination)
	        {
	            _targetPos = _target.transform.position;
	            RotateTowardsTarget();
	        }
            //_targetPos = _target.transform.position;
            //RotateTowardsTarget();
            //float step = _enemySpeed * Time.deltaTime;
            //transform.position = Vector2.MoveTowards(transform.position, _targetPos, step);
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 vectorToTarget = _targetPos - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotationSpeed);
    }

	IEnumerator DoCheck()
	{
		for (; ;)
		{
			foreach (Weapon wep in _inventory_manager.Items)
			{
				if (wep != null)
					wep.Action();
			}
			yield return new WaitForSeconds(1f);
		}
	}
}
