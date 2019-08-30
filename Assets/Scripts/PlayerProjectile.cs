using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 shootingDirection = Vector3.right;

    float flyTime;
    float currentFlyTime = 0;
    float speed;
    int damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (currentFlyTime < flyTime)
        {
            if (shootingDirection != Vector3.right)
            {
                currentFlyTime += Time.fixedDeltaTime;
                rb.velocity = speed * shootingDirection;
            }
        }
        else
        {
            return;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyAI>())
        {
            collision.GetComponent<EnemyAI>().EnemyTakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage) { this.damage = damage; }
    public void SetSpeed(float speed) { this.speed = speed; }
    public void SetShootingDirection(Vector3 dir) { shootingDirection = dir; }
    public void SetFlytime(float flyTime) { this.flyTime = flyTime; }
}
