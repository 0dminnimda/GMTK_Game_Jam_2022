using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsMoving : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 0.03f;
    public int LightFrequency = 100;

    [SerializeField]
    private int timer = 0;

    [SerializeField]
    private bool timerGrow = true;

    void Update()
    {
        if (timer >= LightFrequency)
        {
            timerGrow = false;
            direction *= -1;
        }
        else if (timer <= 0)
            timerGrow = true;

        transform.Translate(direction.normalized * speed);

        if (timerGrow)
            timer += 1;
        else
            timer -= 1;
    }
}
