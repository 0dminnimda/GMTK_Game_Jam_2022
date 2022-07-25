using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandSpriteStart : MonoBehaviour
{
    [SerializeField]
    private Sprite[] possible;
    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = possible[Random.Range(0, possible.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
