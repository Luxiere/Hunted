using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timedStep = .5f;
    [SerializeField] AudioClip[] clips;

    Rigidbody2D rb;
    Animator animator;
    PlayerDash playerDash;
    GameManager gm;

    float currentTimedStep;
    float currentMoveSpeed;
    float controlThrowHorizontal;
    float controlThrowVertical;

    bool alive = true;
    bool dashable = true;
    bool isDashing = false;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
        animator = GetComponent<Animator>();
        currentMoveSpeed = moveSpeed;
        currentTimedStep = 0;
    }

    void Update()
    {
        if (alive)
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
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Movement()
    {
        controlThrowHorizontal = Input.GetAxis("Horizontal");
        controlThrowVertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(controlThrowHorizontal * currentMoveSpeed, controlThrowVertical * currentMoveSpeed);

        currentTimedStep -= Time.deltaTime;
    }

    private void Direction()                
    {
        if(Mathf.Approximately(rb.velocity.x, 0f) && Mathf.Approximately(rb.velocity.y, 0f))
        {
            animator.SetBool("isRunning", false);
            return;
        }
        else
        {
            if (currentTimedStep <= 0)
            {
                AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], Camera.main.transform.position, PlayerPrefsController.GetSoundVolume());
                currentTimedStep = timedStep;
            }
        }

        if (Mathf.Approximately(controlThrowHorizontal, 0f))
        {
            if(controlThrowVertical > 0) { playerDash.DashDirection(90); animator.SetTrigger("runningNorth"); }
            else { playerDash.DashDirection(270); animator.SetTrigger("runningSouth"); }
        }
        else if(Mathf.Approximately(controlThrowVertical, 0f))
        {
            if(controlThrowHorizontal > 0) { playerDash.DashDirection(0); animator.SetTrigger("runningEast"); }
            else { playerDash.DashDirection(180); animator.SetTrigger("runningWest"); }
        }
        else
        {
            float dashAngle = Mathf.Atan2(controlThrowVertical, controlThrowHorizontal) * Mathf.Rad2Deg;
            playerDash.DashDirection(dashAngle);
            if (Mathf.Abs(controlThrowVertical) > Mathf.Abs(controlThrowHorizontal))
            {
                if (controlThrowVertical > 0) { animator.SetTrigger("runningNorth"); }
                else { animator.SetTrigger("runningSouth"); }
            }
            else if (Mathf.Abs(controlThrowHorizontal) > Mathf.Abs(controlThrowVertical))
            {
                if (controlThrowHorizontal > 0) { animator.SetTrigger("runningEast"); }
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

    public void Dead()
    {
        animator.SetTrigger("dead");
        alive = false;
    }

    public void LoseScreen()
    {
        gm.HandleLoseCondition();
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
