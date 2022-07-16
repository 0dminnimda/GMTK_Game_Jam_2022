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
    private List<Weapon> _weapons;

    private GameObject _target;
    private Vector3 _targetPos;

    // Start is called before the first frame update
    void Start()
    {
        _target = FindObjectOfType<MainCharacter>().gameObject;

        StartCoroutine(nameof(DoCheck));
    }

    // Update is called once per frame
    void Update()
    {
        _targetPos = _target.transform.position;

        RotateTowardsTarget();

        float step = _enemySpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _targetPos, step);
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
                wep.Action();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
