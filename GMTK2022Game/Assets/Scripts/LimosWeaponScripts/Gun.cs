using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bigGunBullet;
    public GameObject gunBullet;
    public Transform bulletSpawnpoint;
    public float gunCd;
    private float timer = 0;
    void Start()
    {
        Instantiate(bigGunBullet, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
    }
    void Update()
    {
        if(timer > gunCd)
        {
            Instantiate(gunBullet, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}
