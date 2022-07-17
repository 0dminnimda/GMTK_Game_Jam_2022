using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Healthbar : MonoBehaviour
{
    public float epsilon = 0.01f;

	[SerializeField]
	private Health health;

	[SerializeField]
	private Transform healthbar;
	[SerializeField]
	private Transform healthbarPivot;
	[SerializeField]
	private Transform healthdropPivot;
	[SerializeField]
	private float healthdropSpeed;
	[SerializeField]
	private Vector3 followOffset;

	void Update()
	{
		if (gameObject != null)
		{
			healthbar.transform.position = gameObject.transform.position + followOffset;
			healthbar.transform.rotation = Quaternion.identity;
		}

		healthbarPivot.localScale = new Vector3((float)health.CurrentHealth / (float)health.CurrentMaxHealth, 1f, 1f);
		healthdropPivot.localScale = Vector3.Lerp(healthdropPivot.localScale, new Vector3((float)health.CurrentHealth / (float)health.CurrentMaxHealth, 1f, 1f), healthdropSpeed * Time.deltaTime);

		if (healthdropPivot.localScale.x < epsilon)
		{
			healthbarPivot.localScale = Vector3.zero;
			healthdropPivot.localScale = Vector3.zero;

			health.Die();
			Destroy(gameObject);
		}
	}
}
