using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _lerpPower;
    void FixedUpdate()
    {
        if (_target == null)
            return;

        transform.position = Vector3.Lerp(transform.position, new Vector3(_target.position.x, _target.position.y, transform.position.z), _lerpPower * Time.fixedDeltaTime);
    }
}
