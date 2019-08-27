using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb;
    PlayerDash playerDash;

    bool dashable = true;
    bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
    }

    void Update()
    {
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

    private void Direction()
    {
        if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon || Mathf.Abs(rb.velocity.y) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), Mathf.Sign(rb.velocity.y));
        }        
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
