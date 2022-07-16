using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
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
        Instantiate(_projectilePrefab, position: gameObject.transform.position, rotation: gameObject.transform.rotation);
	}
}
