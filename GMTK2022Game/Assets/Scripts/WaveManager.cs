using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private Bounds _bounds;

    [SerializeField]
    private GameObject[] _enemies;

    private Vector3[] _enemySpawnPositions;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    public class Wave
    {
        public ChanceTable table;
        public float delay;

        public Wave(ChanceTable table, float delay)
        {
            this.table = table;
            this.delay = delay;
        }
    }

    private Wave[] waves;

    public float timeBeforeFirstWave = 15f;
    public float timeBetweenWaves = 10f;
    public float waveCountdown;
    private bool spawningWave = false;
    public int waveInd;

    void Awake()
    {
        var table1 = new ChanceTable(new uint[] { 10, 3, 0, 0, 0 });
        var table2 = new ChanceTable(new uint[] { 7, 7, 3, 0, 0 });
        var table3 = new ChanceTable(new uint[] { 5, 10, 5, 3, 0 });
        var table4 = new ChanceTable(new uint[] { 3, 7, 7, 5, 3 });
        var table5 = new ChanceTable(new uint[] { 0, 5, 10, 7, 5 });
        var table6 = new ChanceTable(new uint[] { 0, 3, 7, 10, 7 });
        var table7 = new ChanceTable(new uint[] { 0, 0, 5, 7, 10 });

        waves = new[]
        {
            new Wave(table1, 0.1f),

            new Wave(table2, 0.1f),

            new Wave(table3, 0.1f),

            new Wave(table4, 0.1f),

            new Wave(table5, 0.1f),

            new Wave(table6, 0.1f),

            new Wave(table7, 0.1f),
        };
    }

	public void Start()
	{
        StartSpawn();
    }

	public void StartSpawn()
    {
        PrepareEnemySpawnPositions();
        //StartCoroutine(nameof(CheckForWave));
    }

	private void Update()
	{
        HandleWave();
    }

	/*private IEnumerator CheckForWave()
	{
        for(; ; )
		{
            HandleWave();
            yield return new WaitForSeconds(0.1f);
		}
	}*/

    private void PrepareEnemySpawnPositions()
    {
        var positions = new List<Vector3>();
        var colliders = _enemies.Select(x => x.GetComponent<Collider2D>()).ToList();
        var maxSize = Vector3.zero;
        colliders.ForEach(x =>
        {
            if (Vector3.Magnitude(x.bounds.size) > Vector3.Magnitude(maxSize))
                maxSize = x.bounds.size;
        });

        int i = 0;
        while (i < MaxNumberOfEnemies())
		{
            Vector2 pos = RandomPosition(_bounds);
            var col = Physics2D.OverlapBox(pos, maxSize, 0);
            if (!col)
                positions.Add(new Vector3(pos.x, pos.y, 0));
            else
                continue;
            i++;
        }

        _enemySpawnPositions = positions.ToArray();
        waveInd = 0;
        waveCountdown = timeBeforeFirstWave;
    }
    private Vector2 RandomPosition(Bounds bounds)
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }

    public void UpdateSpawn()
    {
        HandleWave();
    }

    void HandleWave()
    {
        if (waveCountdown <= 0)
        {
            if (!spawningWave)
            {
                StartCoroutine(SpawnEnemies());
                waveInd++;
                waveCountdown = timeBetweenWaves;
            }
        }
        else
            waveCountdown -= Time.deltaTime;
    }

    IEnumerator SpawnEnemies()
    {
        spawningWave = true;

        RemoveDestroyedEnemies();

        Wave wave = GetWave();
        for (int i = 0; i < MaxNumberOfEnemies(); i++)
        {
            SpawnEnemy(i, wave);
            // Destroy(o, 4f);
            yield return new WaitForSeconds(wave.delay);
        }

        RemoveDestroyedEnemies();

        spawningWave = false;
    }

    Wave GetWave()
    {
        if (waveInd >= waves.Length)
        {
            Time.timeScale = 0;
            return waves[waves.Length - 1];
        }

        return waves[waveInd];
    }

    public GameObject GetClosestEnemy(Vector3 point)
    {
        RemoveDestroyedEnemies();

        // Vector3 nearestPosition = Vector3.zero;
        GameObject nearest = null;
        float nearestDistance = float.PositiveInfinity;
        int instancesCount = spawnedEnemies.Count;
        int i = 0;
        for (; i < instancesCount; i++)
        {
            GameObject next = spawnedEnemies[i];
            Vector3 nextPosition = next.transform.position;
            float dist = (point - nextPosition).sqrMagnitude;
            // float dist = Vector3.Distance(point, nextPosition);
            if (dist < nearestDistance)
            {
                nearest = next;
                // nearestPosition = next.transform.position;
                nearestDistance = dist;
            }
        }

        return nearest;
    }

    void RemoveDestroyedEnemies()
    {
        spawnedEnemies.RemoveAll(s => s == null);
    }

    GameObject SpawnEnemy(int ind, Wave wave)
    {
        int i = 0;
        while(i < MaxNumberOfEnemies())
		{
            Vector3 pos = _enemySpawnPositions[Random.Range(0, _enemySpawnPositions.Length)];
            var randenemy = wave.table.GetRandomItem(ref _enemies);
            if (Physics2D.OverlapBox(pos, randenemy.GetComponent<Collider2D>().bounds.size, 0))
                continue;

            var enemy = Instantiate(randenemy, pos, Quaternion.identity);
            spawnedEnemies.Add(enemy);

            return enemy;
        }
        return null;
    }

    private int MaxNumberOfEnemies() => 20;
}
