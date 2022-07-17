using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    public GameObject minigunBullet;
    public Transform bulletSpawnpoint;
    public float gunCdBegin;
    public float gunCdBase;
    public float gunCdChange;
    public float spread;
    private float timer = 0;
    private float gunCd;
    void Start()
    {
        Instantiate(minigunBullet, bulletSpawnpoint.position, Quaternion.Euler(Vector3.forward * (bulletSpawnpoint.rotation.eulerAngles.z + Random.Range(-spread, spread))));
        gunCd = gunCdBegin;
    }
    void Update()
    {
        gunCd = Mathf.Lerp(gunCd, gunCdBase, gunCdChange * Time.deltaTime);
        if (timer > gunCd)
        {
            Instantiate(minigunBullet, bulletSpawnpoint.position, Quaternion.Euler(Vector3.forward * (bulletSpawnpoint.rotation.eulerAngles.z + Random.Range(-spread, spread))));
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}
