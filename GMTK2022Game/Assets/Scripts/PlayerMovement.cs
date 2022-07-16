using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
    private CharacterController _controller;

    [SerializeField]
    private float _playerSpeed = 1f;

    private void Start()
    {
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        _controller.Move(move * Time.deltaTime * _playerSpeed);
    }
}
