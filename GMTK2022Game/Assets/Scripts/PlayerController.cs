using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
    private Rigidbody2D _rigidBody2D;

    [SerializeField]
    private float _playerSpeed = 1f;

    enum RotationMethods {None, Movement, MovementNoSmoothing, Mouse, Spinning};

    [SerializeField]
    private RotationMethods _rotationMethod;

    [SerializeField]
    private float _spinningSpeed;

    private void Start()
    {
    }
    private void Update()
    {
        Rotation();
    }

    void FixedUpdate()
    {
        Vector2 move = Vector3.Normalize(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        _rigidBody2D.AddForce(move * Time.fixedDeltaTime * _playerSpeed, ForceMode2D.Impulse);
    }

    private void Rotation()
    {
        if (_rotationMethod == RotationMethods.Movement)
        {
            Vector3 diff = _rigidBody2D.velocity;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            if (diff.magnitude != 0f)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), 35f * Time.deltaTime);
        }
        else if (_rotationMethod == RotationMethods.MovementNoSmoothing)
        {
            Vector3 diff = _rigidBody2D.velocity;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            if (diff.magnitude != 0f)
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else if (_rotationMethod == RotationMethods.Mouse)
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else if(_rotationMethod == RotationMethods.Spinning)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * _spinningSpeed);
        }
    }
}
