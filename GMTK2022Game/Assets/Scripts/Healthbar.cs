using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private Health health;

    [SerializeField]
    private Transform healthbarPivot;
    [SerializeField]
    private Transform healthdropPivot;
    [SerializeField]
    private float healthdropSpeed;
    [SerializeField]
    private Transform followObject;
    [SerializeField]
    private Vector3 followOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = followObject.position + followOffset;
        transform.rotation = Quaternion.identity;
        if (health != null)
        {
            healthbarPivot.localScale = new Vector3((float)health.CurrentHealth / (float)health.CurrentMaxHealth, 1f, 1f);
            healthdropPivot.localScale = Vector3.Lerp(healthdropPivot.localScale, new Vector3((float)health.CurrentHealth / (float)health.CurrentMaxHealth, 1f, 1f), healthdropSpeed * Time.deltaTime);
        }
        else
        {
            healthbarPivot.localScale = Vector3.zero;
            healthdropPivot.localScale = Vector3.zero;
        }
    }
}
