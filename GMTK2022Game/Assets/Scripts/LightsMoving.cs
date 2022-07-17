using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class LightsMoving : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    private int timer;
    private bool timerGrow;
    public int LightFrequency;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.03f;
        direction.x = 1;
        timer = 0;
        timerGrow = true;
        LightFrequency = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerGrow == true) timer += 1;

        if (timer == LightFrequency)
        {
            timerGrow = false;
            direction.x *= -1;
        }
        print(timer);
        if (timerGrow == false) timer -= 1;
        if (timer == 0) timerGrow = true;
        transform.Translate(direction.normalized * speed);
    }
}
