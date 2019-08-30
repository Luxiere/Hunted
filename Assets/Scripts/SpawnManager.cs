using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
	// private variables
	private Vector2 _randomPoint;

	private List<GameObject> _enemies = new List<GameObject>();

	[Header("Spawner Properties")]
	[SerializeField] private float _randX;
	[SerializeField] private float _randY;
	[SerializeField] private int _maxEnemyCount;
	[SerializeField] private GameObject _enemyPrefab;

	private void Start()
	{
		SpawnEnemies();
	}

	private void Update()
	{
		_randomPoint = new Vector2(Random.Range(-_randX, _randX), Random.Range(-_randY, _randY));
	}

	private void SpawnEnemies()
	{
		for (int e = 0; e <= _maxEnemyCount - 1; e++)
		{
			_randomPoint = new Vector2(Random.Range(-_randX, _randX), Random.Range(-_randY, _randY));
			var enemy = Instantiate(_enemyPrefab, _randomPoint, Quaternion.identity) as GameObject;
			_enemies.Add(enemy);
		}
	}
=======
=======
>>>>>>> 35f5ef274711dbae319ebfb20a83a891e98a6c15
    // private variables
    private float _spawnTimer = 0f;

    private Vector2 _randomPoint;

    private List<GameObject> _enemies = new List<GameObject>();

    [Header("Spawner Properties")]
    [SerializeField] private float _nextSpawnTime;
    [SerializeField] private float _randX;
    [SerializeField] private float _randY;
    [SerializeField] private int _maxEnemyCount;
    [SerializeField] private GameObject _enemyPrefab;

    private void Start()
    {
        //_randX = Random.Range(-_randX, _randX);
        //_randY = Random.Range(-_randY, _randY);
        _randomPoint = new Vector2(Random.Range(-_randX, _randX), Random.Range(-_randY, _randY));
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f && _enemies.Count < _maxEnemyCount)
        {
            _randomPoint = new Vector2(Random.Range(-_randX, _randX), Random.Range(-_randY, _randY));

            var enemy = Instantiate(_enemyPrefab, _randomPoint, Quaternion.identity) as GameObject;
            _enemies.Add(enemy);
            _spawnTimer = _nextSpawnTime;
        }
    }
<<<<<<< HEAD
>>>>>>> e1d7dd380c67b7264be5c8ea89a34838ddc3c92b
=======
>>>>>>> 35f5ef274711dbae319ebfb20a83a891e98a6c15
}