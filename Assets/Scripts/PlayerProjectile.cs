using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 shootingDirection;
    BoxCollider2D box;

    [SerializeField] bool piercing = false;

    float flyTime;
    float currentFlyTime = 0;
    float speed;
    int damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (currentFlyTime < flyTime)
        {
            currentFlyTime += Time.fixedDeltaTime;
            rb.velocity = speed * shootingDirection;
        }
        else
        {
            box.enabled = false;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyAI>())
        {
            collision.GetComponent<EnemyAI>().EnemyTakeDamage(damage);
            if (!piercing)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (!piercing)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(int damage) { this.damage = damage; }
    public void SetSpeed(float speed) { this.speed = speed; }
    public void SetShootingDirection(Vector3 dir) { shootingDirection = dir; }
    public void SetFlytime(float flyTime) { this.flyTime = flyTime; }
}
