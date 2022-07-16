using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _possibleDynamicObstacles;
    [SerializeField]
    private List<GameObject> _possibleStaticObstacles;

    [SerializeField]
    [Range(0f,1f)]
    private float _densityDynamicObstacles;

    [SerializeField]
    [Range(0f, 1f)]
    private float _densityStaticObstacles;

    [SerializeField]
    private int _passes;

    [SerializeField]
    private Bounds _boundingBox;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _passes; i++)
		{
            // static
            foreach (var obs in _possibleStaticObstacles)
            {
                if(Random.Range(0,1) < _densityStaticObstacles)
				{
                    var collider = obs.GetComponent<Collider2D>();
                    Vector2 size = collider.bounds.size;
                    Vector2 pos = RandomPosition(_boundingBox);
                    float angle = RandomAngle();
                    var col = Physics2D.OverlapBox(pos, size, angle);
                    if (col == null)
                        Instantiate(obs, pos, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
                }
            }

            // dynamic
            foreach (var obs in _possibleDynamicObstacles)
            {
                if (Random.Range(0, 1) < _densityDynamicObstacles)
                {
                    var collider = obs.GetComponent<Collider2D>();
                    Vector2 size = collider.bounds.size;
                    Vector2 pos = RandomPosition(_boundingBox);
                    float angle = RandomAngle();
                    var col = Physics2D.OverlapBox(pos, size, angle);
                    if (col == null)
                        Instantiate(obs, pos, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
                }
            }
        }
    }

    private Vector2 RandomPosition(Bounds bounds)
	{
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
	}

    private float RandomAngle() => Random.Range(0, 2 * Mathf.PI);
}
