using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimosPlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody2D;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Health _health;
    [SerializeField]
    private Transform _rotatingPart;
    [SerializeField]
    private Transform _weaponSpawnPart;
    [SerializeField]
    private Transform _currentWeaponicon;
    [SerializeField]
    private Transform _nextWeaponicon;
    [SerializeField]
    private float _playerSpeed = 1f;
    [SerializeField]
    private float _sideChangeTime;
    enum RotationMethods { None, Movement, MovementNoSmoothing, Mouse, Spinning };
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
    [SerializeField]
    private Sprite[] _movementSprites;

    public bool alwaysChanges;

    public GameObject[] weaponPrefabs;
    public Sprite[] weaponSprites;

    private bool _rolling;
    private bool _fullRoll;
    private bool _moving;
    private int _movestate = 0; // 0 - Top side, 1 - Closer to top side, 2 - Closer to new side
    private float _siderolltimer = 0f;
    private Vector2 _rolldir;
    private SpriteRenderer _currentWeaponIconSR;
    private SpriteRenderer _nextWeaponIconSR;
    private int _weaponsAmmount;
    private int _currentWeaponId;
    private int _inuseWeaponId;
    private int _nextWeaponId;
    private GameObject _WeaponObj;
    private Color _defaultIconColor;
    private int _weaponBeforeRoll = 0;

    private void Start()
    {
        _currentWeaponIconSR = _currentWeaponicon.GetComponent<SpriteRenderer>();
        _nextWeaponIconSR = _nextWeaponicon.GetComponent<SpriteRenderer>();
        _weaponsAmmount = weaponSprites.Length;
        _nextWeaponId = Random.Range(0, _weaponsAmmount);
        _defaultIconColor = _currentWeaponIconSR.color;
        NextSide();
        _WeaponObj = Instantiate(weaponPrefabs[_currentWeaponId], _weaponSpawnPart);
        _inuseWeaponId = _currentWeaponId;
    }
    private void Update()
    {
        _moving = _rigidBody2D.velocity.magnitude > 1f;
        if (_moving)
        {
            if(_siderolltimer <= 0f)
            {
                //NextMoveState;
                if (_movestate == 2)
                {
                    _movestate = 0;
                    NextSide();
                }
                else
                    _movestate++;
                UpdateMovingSprite(_movestate);
                _siderolltimer = _sideChangeTime;
            }
            else
            {
                /*
                if (_fullRoll)
                    _siderolltimer -= Time.deltaTime * (_playerSpeed / 100f) * _rollSpeedMultiplier;
                else
                    _siderolltimer -= Time.deltaTime * (_playerSpeed / 100f);
                */
                _siderolltimer -= Time.deltaTime * _rigidBody2D.velocity.magnitude; //This one looks 50 times better so i'll leave it
            }
        }
        else
        {
            _siderolltimer = 0f;
            if(_movestate != 0)
            {
                if (_movestate == 2)
                    NextSide();
                _movestate = 0;
            }
            UpdateMovingSprite(_movestate);
        }
        if (!_rolling)
        {
            _currentWeaponIconSR.color = Color.Lerp(_currentWeaponIconSR.color, _defaultIconColor, 5f * Time.deltaTime);
            _nextWeaponIconSR.color = Color.Lerp(_currentWeaponIconSR.color, _defaultIconColor, 5f * Time.deltaTime);
        }
        UpdateWeaponPivot();
        Rotation();
        if (!_rolling && Input.GetKeyDown(KeyCode.Space))
            Dodgeroll();
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
    private void UpdateWeaponPivot()
    {
        if ((alwaysChanges && _movestate == 2) || _rolling)
        {
            _weaponSpawnPart.localScale = Vector3.Lerp(_weaponSpawnPart.localScale, Vector3.zero, 35f * Time.deltaTime);
        }
        else
        {
            _weaponSpawnPart.localScale = Vector3.Lerp(_weaponSpawnPart.localScale, Vector3.one, 30f * Time.deltaTime);
        }
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _weaponSpawnPart.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        _weaponSpawnPart.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
    private void UpdateMovingSprite(int movestate)
    {
        switch (movestate)
        {
            case 0:
                _spriteRenderer.sprite = _movementSprites[0];
                _spriteRenderer.flipY = false;
                _currentWeaponicon.localPosition = Vector3.zero;
                _currentWeaponicon.localScale = Vector3.one;
                _currentWeaponicon.gameObject.SetActive(true);
                _nextWeaponicon.localPosition = Vector3.zero;
                _nextWeaponicon.localScale = Vector3.one;
                _nextWeaponicon.gameObject.SetActive(false);
                break;
            case 1:
                _spriteRenderer.sprite = _movementSprites[1];
                _currentWeaponicon.localPosition = Vector3.up * 0.25f;
                _currentWeaponicon.localScale = new Vector3(1f, 0.9f, 1f);
                _currentWeaponicon.gameObject.SetActive(true);
                _nextWeaponicon.localPosition = Vector3.up * -0.5f;
                _nextWeaponicon.localScale = new Vector3(1f, 0.25f, 1f);
                _nextWeaponicon.gameObject.SetActive(true);
                _spriteRenderer.flipY = false;
                break;
            case 2:
                _spriteRenderer.sprite = _movementSprites[1];
                _currentWeaponicon.localPosition = Vector3.up * 0.5f;
                _currentWeaponicon.localScale = new Vector3(1f, 0.25f, 1f);
                _currentWeaponicon.gameObject.SetActive(true);
                _nextWeaponicon.localPosition = Vector3.up * -0.25f;
                _nextWeaponicon.localScale = new Vector3(1f, 0.9f, 1f);
                _nextWeaponicon.gameObject.SetActive(true);
                _spriteRenderer.flipY = true;
                break;
        }
    }
    private void NextSide()
    {
        Debug.Log("Next Side");
        _currentWeaponId = _nextWeaponId;
        if (!_rolling)
        {
            while (_nextWeaponId == _currentWeaponId)
            {
                _nextWeaponId = Random.Range(0, _weaponsAmmount);
            }
        }
        else
        {
            while (_nextWeaponId == _weaponBeforeRoll)
            {
                _nextWeaponId = Random.Range(0, _weaponsAmmount);
            }
        }
        if (alwaysChanges)
        {
            if (_WeaponObj != null)
            {
                Destroy(_WeaponObj);
            }
            if (!_rolling)
            {
                _WeaponObj = Instantiate(weaponPrefabs[_currentWeaponId], _weaponSpawnPart); //This one was TOO cheaty
                _inuseWeaponId = _currentWeaponId;
            }
        }
        _currentWeaponIconSR.sprite = weaponSprites[_currentWeaponId];
        _nextWeaponIconSR.sprite = weaponSprites[_nextWeaponId];
    }
    private void Dodgeroll()
    {
        if (_rigidBody2D.velocity.magnitude > 0.5f)
        {
            _rolldir = _rigidBody2D.velocity.normalized;
            _weaponBeforeRoll = _inuseWeaponId;
            _rolling = true;
            _fullRoll = true;
            _health.ignoreDamage = true;
            gameObject.layer = 3;

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
                _rotatingPart.rotation = Quaternion.Lerp(_rotatingPart.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), 35f * Time.deltaTime);
        }
        else if (_rotationMethod == RotationMethods.MovementNoSmoothing)
        {
            Vector3 diff = _rigidBody2D.velocity;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            if (diff.magnitude != 0f)
                _rotatingPart.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else if (_rotationMethod == RotationMethods.Mouse)
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _rotatingPart.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            _rotatingPart.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else if (_rotationMethod == RotationMethods.Spinning)
        {
            _rotatingPart.Rotate(Vector3.forward * Time.deltaTime * _spinningSpeed);
        }
    }
    private IEnumerator DodgerollIEnum()
    {
        float timer = 0f;
        Color characterColor = _spriteRenderer.color;
        _currentWeaponIconSR.color = Color.red;
        _nextWeaponIconSR.color = Color.red;
        if (_WeaponObj != null)
        {
            Destroy(_WeaponObj);
        }
        while (timer < _rollTime)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, new Color(characterColor.r * 0.6f, characterColor.g * 0.6f, characterColor.b * 0.6f, characterColor.a), 30f * Time.deltaTime);
            _rigidBody2D.AddForce(_rolldir * Time.deltaTime * _playerSpeed * _rollSpeedMultiplier * (((_rollTime - timer) / _rollTime) + 1), ForceMode2D.Impulse);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _fullRoll = false;
        _health.ignoreDamage = false;
        gameObject.layer = 0;
        while (timer < _rollTime + _rollcd)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, characterColor, 35f * Time.deltaTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _WeaponObj = Instantiate(weaponPrefabs[_currentWeaponId], _weaponSpawnPart);
        _inuseWeaponId = _currentWeaponId;
        _rolling = false;
    }
}
