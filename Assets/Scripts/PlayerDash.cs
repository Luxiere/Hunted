using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] GameObject dashTarget;
    [SerializeField] GameObject dashDirection;

    [Header("Quirk properties")]
    [SerializeField] float knifeConeAngle;


    Rigidbody2D rb;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    Vector2 dashTargetPos;

    float dashSpeed = 10f;
    float dashTime = .25f;
    float dashCooldown = 2f;
    float currentDashTime = 0f;
    float dashCooldownTimer;

    bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
        dashSpeed = playerShooting.CurrentWeapon().GetDashSpeed();
        dashTime = playerShooting.CurrentWeapon().GetDashTime();
        dashCooldown = playerShooting.CurrentWeapon().GetDashCooldown();
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

    public void DashDirection(float degree)
    {
        dashDirection.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, degree));
    }

    public void KnifeQuirk()
    {

    }

    public void BowQuirk()
    {
        gameObject.layer = LayerMask.GetMask("Invulnerable");
        if(currentDashTime > dashTime)
        {
            gameObject.layer = LayerMask.GetMask("Player");
        }
    }

    public void 
}
