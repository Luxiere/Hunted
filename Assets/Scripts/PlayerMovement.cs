using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb;
    Animator animator;
    PlayerDash playerDash;

    bool dashable = true;
    bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Direction();
        Dash();
        DashControl();
        DashCooldown();
        if (!isDashing)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float controlThrowHorizontal = Input.GetAxis("Horizontal");
        float controlThrowVertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(controlThrowHorizontal * moveSpeed, controlThrowVertical * moveSpeed);
    }

    private void Direction()                
    {
        if(rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            return;
        }

        if (rb.velocity.x == 0)
        {
            if(rb.velocity.y > 0) { playerDash.DashDirection(90); }
            else { playerDash.DashDirection(270); }
            // play animation here
        }
        else if(rb.velocity.y == 0)
        {
            if(rb.velocity.x > 0) { playerDash.DashDirection(0); }
            else { playerDash.DashDirection(180); }
            // play animation here
        }
        else
        {
            float dashAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            playerDash.DashDirection(dashAngle);
        }
    }

    private void DashControl()
    {
        if (Input.GetButtonDown("Dash"))
        {
            if (dashable)
            {
                isDashing = true;
                dashable = false;
                playerDash.CacheDashTarget();
            }
        }
    }

    private void DashCooldown()
    {
        if (!dashable)
        {
            playerDash.DashCooldown();
        }
    }

    private void Dash()
    {
        if (isDashing)
        {
            playerDash.Dash();
        }
    }

    public bool GetIsDashing()
    {
        return isDashing;               //use this if you need to check character dashing
    }

    public void SetIsDashing(bool isDashing)
    {
        this.isDashing = isDashing;
    }

    public void SetDashable(bool dashable)
    {
        this.dashable = dashable;
    }
}
