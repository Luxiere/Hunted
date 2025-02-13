﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageBlast : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
        }
    }
}
