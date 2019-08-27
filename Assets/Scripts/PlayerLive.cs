using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLive : MonoBehaviour
{
    private event Action onDead; 

    public void OnDead(Action a)
    {
        onDead += a;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            onDead();
        }
    }
}
