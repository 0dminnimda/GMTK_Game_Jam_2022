using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject shotgunBullet;
    public int firstburstCount;
    public int burstCount;
    public Transform bulletSpawnpoint;
    public float gunCd;
    public float spread;
    private float timer = 0;
    void Start()
    {
        for(int i = 0; i < firstburstCount; i++)
        {
            Instantiate(shotgunBullet, bulletSpawnpoint.position, Quaternion.Euler(Vector3.forward * (bulletSpawnpoint.rotation.eulerAngles.z + Random.Range(-spread, spread))));
        }
    }
    void Update()
    {
        if (timer > gunCd)
        {
            for (int i = 0; i < burstCount; i++)
            {
                Instantiate(shotgunBullet, bulletSpawnpoint.position, Quaternion.Euler(Vector3.forward * (bulletSpawnpoint.rotation.eulerAngles.z + Random.Range(-spread, spread))));
            }
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}