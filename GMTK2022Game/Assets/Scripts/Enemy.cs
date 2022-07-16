using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private List<Weapon> _weapons;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(DoCheck));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoCheck()
    {
        for (; ;)
        {
            foreach (Weapon wep in _weapons)
            {
                wep.Shoot();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
