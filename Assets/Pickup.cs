using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    PickupManager pm;

    void Start()
    {
        pm = FindObjectOfType<PickupManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pm.PickedUp();

            Destroy(gameObject);
        }
    }
}
