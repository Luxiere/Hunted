using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // private variables
    private float _spawnTimer = 0f;

    private Vector2 _randomPoint;
    private List<Vector2> randomPos = new List<Vector2>();
    private Bounds bounds;

    [Header("Spawner Properties")]
    [SerializeField] private GameObject _enemyPrefab;

    public void Spawn(PolygonCollider2D[] spawnArea, int _maxEnemyCount)
    {
        while (randomPos.Count < _maxEnemyCount)
        {
            bounds = spawnArea[Random.Range(0, spawnArea.Length)].bounds;
            _randomPoint = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
            if (!randomPos.Contains(_randomPoint))
            {
                randomPos.Add(_randomPoint);
            }
        }
        foreach (Vector2 _randomPoint in randomPos)
        {
            var enemy = Instantiate(_enemyPrefab, _randomPoint, Quaternion.identity) as GameObject;
            enemy.transform.position = _randomPoint;
        }
    }
}