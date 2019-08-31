using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D[] spawnArea;
    [SerializeField] private int _maxEnemyCount;

    SpawnManager sm;
    GameManager gm;

    private void Start()
    {
        sm = FindObjectOfType<SpawnManager>();
        gm = sm.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MusicMaster.FadeIn(gm.fight, gm.chill, .25f);
        sm.Spawn(spawnArea, _maxEnemyCount);
        gm.AddEnemy(_maxEnemyCount);
        Destroy(gameObject);
    }
}
