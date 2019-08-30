using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    SpawnManager sm;

    private void Start()
    {
        sm = FindObjectOfType<SpawnManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sm.Spawn();
        Destroy(gameObject);
    }
}
