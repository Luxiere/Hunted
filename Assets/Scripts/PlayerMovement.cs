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

    float currentMoveSpeed;

    bool dashable = true;
    bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
        animator = GetComponent<Animator>();
        currentMoveSpeed = moveSpeed;
    }

    void Update()
    {
        ResetTrigger();
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
        rb.velocity = new Vector2(controlThrowHorizontal * currentMoveSpeed, controlThrowVertical * currentMoveSpeed);
    }

    private void Direction()                
    {
        if(Mathf.Approximately(rb.velocity.x, 0f) && Mathf.Approximately(rb.velocity.y, 0f))
        {
            animator.SetBool("isRunning", false);
            return;
        }

        if (Mathf.Approximately(rb.velocity.x, 0f))
        {
            if(rb.velocity.y > 0) { playerDash.DashDirection(90); animator.SetTrigger("runningNorth"); }
            else { playerDash.DashDirection(270); animator.SetTrigger("runningSouth"); }
        }
        else if(Mathf.Approximately(rb.velocity.y, 0f))
        {
            if(rb.velocity.x > 0) { playerDash.DashDirection(0); animator.SetTrigger("runningEast"); }
            else { playerDash.DashDirection(180); animator.SetTrigger("runningWest"); }
        }
        else
        {
            float dashAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            playerDash.DashDirection(dashAngle);
            if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
            {
                if (rb.velocity.y > 0) { animator.SetTrigger("runningNorth"); }
                else { animator.SetTrigger("runningSouth"); }
            }
            else if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
            {
                if (rb.velocity.x > 0) { animator.SetTrigger("runningEast"); }
                else { animator.SetTrigger("runningWest");  }
            }
        }
    }

    private void ResetTrigger()
    {
        animator.SetBool("isRunning", true);
        animator.ResetTrigger("runningNorth");
        animator.ResetTrigger("runningSouth");
        animator.ResetTrigger("runningEast");
        animator.ResetTrigger("runningWest");
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
