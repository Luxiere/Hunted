using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashTime = .25f;
    [SerializeField] float dashCooldown = 2f;
    [SerializeField] GameObject dashTarget;

    Rigidbody2D rb;
    PlayerMovement playerMovement;
    Vector2 dashTargetPos;

    float currentDashTime = 0f;
    float dashCooldownTimer;

    bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }
       
    public void Dash()
    {
        currentDashTime += Time.deltaTime;
        if (currentDashTime > dashTime)
        {
            currentDashTime = 0;
            playerMovement.SetIsDashing(false);
        }
        float perc = currentDashTime / dashTime;
        transform.position = Vector2.Lerp(transform.position, dashTargetPos, perc);
    }

    public void CacheDashTarget()
    {
        dashTargetPos = dashTarget.transform.position;
    }

    public void DashCooldown()
    {
        dashCooldownTimer -= Time.deltaTime;
        if(dashCooldownTimer <= Mathf.Epsilon)
        {
            dashCooldownTimer = dashCooldown;
            playerMovement.SetDashable(true);
        }
    }
}
