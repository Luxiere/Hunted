using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] GameObject dashTarget;
    [SerializeField] GameObject dashDirection;

    [Header("Quirk properties")]
    [SerializeField] float knifeConeAngle;
    [SerializeField] GameObject spearShadowPrefab;

    Rigidbody2D rb;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    Vector2 dashTargetPos;

    float dashSpeed;
    float dashTime = .25f;
    float dashCooldown = 2f;
    float currentDashTime = 0f;
    float dashCooldownTimer;

    bool isDashing = false;

    private void Awake()
    {
    }
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

        switch (playerShooting.weapon)
        {
            case Weapon.Knives:
                KnifeQuirk();
                break;
            case Weapon.Bow:
                BowQuirk();
                break;
            case Weapon.Spears:
                SpearQuirk();
                break;
        }
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
        int remainingKnives = playerShooting.RemainingBulletInMag();
        float knifeAngle = knifeConeAngle / remainingKnives;
        float knife1Angle = (180 - knifeConeAngle) / 2 + dashDirection.transform.rotation.z - 90;

        for (int i = 0; i < remainingKnives; i++)
        {
            GameObject knives = Instantiate(playerShooting.CurrentWeapon().GetWeapon().gameObject, transform.position, Quaternion.identity) as GameObject;
            knives.transform.rotation = Quaternion.Euler(knives.transform.rotation.x, knives.transform.rotation.y, knife1Angle + i * knifeAngle);
            knives.transform.parent = knives.transform;
            knives.GetComponent<PlayerProjectile>().SetSpeed (playerShooting.CurrentWeapon().GetProjectileSpeed());
            knives.GetComponent<PlayerProjectile>().SetFlytime(playerShooting.CurrentWeapon().GetFlyTime());
            knives.GetComponent<PlayerProjectile>().SetDamage(Mathf.RoundToInt(playerShooting.CurrentWeapon().GetDamage() * 1));
        }
        playerShooting.EmptyChamber();
    }

    public void BowQuirk()
    {
        gameObject.layer = 31;
        if(currentDashTime > dashTime)
        {
            gameObject.layer = 8;
        }
    }

    public void SpearQuirk()
    {
        Instantiate(spearShadowPrefab, transform.position, Quaternion.identity);
    }
}
