using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InventoryManager _character;

    [SerializeField]
    private Rigidbody2D _rigidBody2D;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Health _health;

    [SerializeField]
    private float _playerSpeed = 1f;

    enum RotationMethods {None, Movement, MovementNoSmoothing, Mouse, Spinning};

    [SerializeField]
    private RotationMethods _rotationMethod;

    [SerializeField]
    private float _spinningSpeed;


    [SerializeField]
    private float _rollSpeedMultiplier;
    [SerializeField]
    private float _rollTime;
    [SerializeField]
    private float _rollcd;

    private bool _rolling;
    private bool _fullRoll;
    private Vector2 _rolldir;

    private void Start()
    {
    }
    private void Update()
    {
        Rotation();
        if (!_rolling && Input.GetKeyDown(KeyCode.LeftShift))
            Dodgeroll();

        if (Input.GetKey(KeyCode.Space))
            _character.Items.ToList().ForEach(x => { if (x != null) x.Action(); });
    }

    void FixedUpdate()
    {
        if (!_fullRoll)
            Movement();
    }
    private void Movement()
    {
        Vector2 move = Vector3.Normalize(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        _rigidBody2D.AddForce(move * Time.fixedDeltaTime * _playerSpeed, ForceMode2D.Impulse);
    }
    private void Dodgeroll()
    {
        if(_rigidBody2D.velocity.magnitude > 0.5f)
        {
            _rolldir = _rigidBody2D.velocity.normalized;
            _rolling = true;
            _fullRoll = true;
            _health.ignoreDamage = true;
            StartCoroutine(DodgerollIEnum());
        }
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
    private IEnumerator DodgerollIEnum()
    {
        float timer = 0f;
        Color characterColor = _spriteRenderer.color;
        while(timer < _rollTime)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, new Color(characterColor.r * 0.7f, characterColor.g * 0.7f, characterColor.b * 0.7f, characterColor.a), 30f * Time.deltaTime);
            _rigidBody2D.AddForce(_rolldir * Time.deltaTime * _playerSpeed * _rollSpeedMultiplier * (((_rollTime - timer) / _rollTime) + 1), ForceMode2D.Impulse);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _fullRoll = false;
        _health.ignoreDamage = false;
        while (timer < _rollTime + _rollcd)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, characterColor, 35f * Time.deltaTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _rolling = false;
    }
}
