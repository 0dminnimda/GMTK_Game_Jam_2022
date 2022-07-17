using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform pivotPoint;
    public float swordcd;
    public float swingTime;
    public float swingSpeed;
    private float swingdir = 1f;
    private float pivAngle = 90f;
    private float timer = 0f;
    void Start()
    {
        StartCoroutine(Swing());
    }

    // Update is called once per frame
    void Update()
    {
        pivotPoint.localScale = Vector3.Lerp(pivotPoint.localScale, Vector3.one, 2f * Time.deltaTime);
        timer += Time.deltaTime;
        pivotPoint.localRotation = Quaternion.Euler(0f, 0f, pivAngle);
        if(timer > swordcd)
        {
            StartCoroutine(Swing());
            timer = 0f;
        }
    }

    IEnumerator Swing()
    {
        pivAngle = 90f * swingdir;
        float timer = 0f;
        while(timer < swingTime)
        {
            pivAngle = Mathf.Lerp(pivAngle, 90f * swingdir * -1f, swingSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        swingdir *= -1;
    }
}
