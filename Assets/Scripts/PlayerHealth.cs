using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth: MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioClip death;
    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Projectile")
        {
            animator.SetTrigger("dead");
            gm.HandleLoseCondition();
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position);
        }
    }
}
