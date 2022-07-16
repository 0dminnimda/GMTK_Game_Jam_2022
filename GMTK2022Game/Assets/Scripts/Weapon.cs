using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private DamageLayer _damageLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
	{
        Debug.Log("Pos spawn: " + gameObject.transform.position.ToString());
        var proj = Instantiate(_projectilePrefab, gameObject.transform.position, gameObject.transform.rotation);
        var script = proj.GetComponent<Projectile>();
        script.SetDamageLayer(_damageLayer);
	}
}
