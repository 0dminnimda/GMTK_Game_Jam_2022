using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
    private Rigidbody2D _rigidBody2D;

    [SerializeField]
    private float _playerSpeed = 1f;

    private void Start()
    {
    }

    void FixedUpdate()
    {
        Vector2 move = Vector3.Normalize(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        _rigidBody2D.AddForce(move * Time.deltaTime * _playerSpeed, ForceMode2D.Impulse);
    }
}
