using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirage : MonoBehaviour
{
    [SerializeField] float existTime = 2f;

    MirageBlast blast;

    private void Start()
    {
        blast = GetComponentInChildren<MirageBlast>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            blast.gameObject.SetActive(true);
            Destroy(gameObject, existTime);
        }
    }
}
