using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth: MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioClip death;

    PlayerMovement player;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("a");

        if (collision.CompareTag("EnemyProjectile"))
        {
            player.Dead();
            animator.SetTrigger("dead");
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, PlayerPrefsController.GetSoundVolume());
        }
    }
}
