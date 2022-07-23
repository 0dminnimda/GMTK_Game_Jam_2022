using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private LayerMask _dragAndDropMask;

    [SerializeField]
    private GameObject _weaponPrefab;
    public void SetWeaponPrefab(GameObject prefab) => _weaponPrefab = prefab;

    [SerializeField]
    private Sprite _pickupSprite;
    public void SetPickupSprite(Sprite sprite) => _pickupSprite = sprite;

    [SerializeField]
    private float _spawnForce;

    [SerializeField] [Range(0f, 1f)]
    private float _mouseOverTransparency;

    [SerializeField] [Range(0f, 1f)]
    private float _mouseDragTransparency;

    private bool _isDragged = false;

    private Vector3 _initialPos;
    private Camera _cam;
    private Vector3 _dragOffset;
    private GameObject _potentialWeaponPivot;

    private InventoryManager _character;
    private void Awake()
    {
        _initialPos = transform.position;
        _cam = Camera.main;
    }
    void Start()
    {
        _renderer.sprite = _pickupSprite;
    }
    private void OnMouseDown()
    {
        _isDragged = true;
        _dragOffset = transform.position - GetMousePos();
    }
    private void OnMouseDrag()
    {
        transform.position = GetMousePos() + _dragOffset;
        _renderer.color = new Color(255f, 255f, 255f, _mouseDragTransparency);
    }
    private void OnMouseUp()
    {
        _isDragged = false;
        if (_potentialWeaponPivot == null)
        {
            _renderer.color = new Color(255f, 255f, 255f, 1f);
        }
        else
        {
            //getting pivot id by getting the last character of it's name SO DON'T RENAME WEAPON PIVOTS!!!
            char lastChar = _potentialWeaponPivot.name[_potentialWeaponPivot.name.Length - 1];
            int index = System.Int32.Parse(lastChar.ToString());

            _character.SpawnWeapon(index, _weaponPrefab);

            Destroy(gameObject);
        }
    }
    private void OnMouseEnter()
    {
        _renderer.color = new Color(255f, 255f, 255f, _mouseOverTransparency);
    }
    private void OnMouseExit()
    {
        _renderer.color = new Color(255f, 255f, 255f, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isDragged)
            return;

        if ((collision.gameObject.layer & _dragAndDropMask) != _dragAndDropMask)
            return;

        if (_potentialWeaponPivot != null)
            _potentialWeaponPivot.GetComponent<SpriteRenderer>().enabled = false;

        //getting maincharacter on the first trigger enter is more efficient than using FindObjectOfType i think
        if (_character == null)
            _character = collision.transform.parent.GetComponentInParent<InventoryManager>();

        _potentialWeaponPivot = collision.gameObject;
        _potentialWeaponPivot.GetComponent<SpriteRenderer>().enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _potentialWeaponPivot && _potentialWeaponPivot != null)
        {
            _potentialWeaponPivot.GetComponent<SpriteRenderer>().enabled = false;
            _potentialWeaponPivot = null;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if pickup left one trigger but still is in another one then that another trigger becomes active
        if (!_isDragged)
            return;

        if (((1 << collision.gameObject.layer) & _dragAndDropMask) == 0)
            return;

        if (_character == null)
            _character = collision.transform.parent.GetComponentInParent<InventoryManager>();

        if (_potentialWeaponPivot == null)
        {
            _potentialWeaponPivot = collision.gameObject;
            _potentialWeaponPivot.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void Push(Vector3 dir)
    { 
		_rb.AddForce(dir * _spawnForce, ForceMode2D.Impulse);
    }
    Vector3 GetMousePos() 
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
