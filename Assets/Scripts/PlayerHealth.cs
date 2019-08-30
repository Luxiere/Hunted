using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth: MonoBehaviour
{
    [SerializeField] Animator animator;
    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Projectile")
        {
            gm.HandleLoseCondition();
        }
    }
}
