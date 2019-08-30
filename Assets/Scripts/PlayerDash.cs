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
    GameObject projectileParent;

    float dashSpeed;
    float dashTime = .25f;
    float dashCooldown = 2f;
    float currentDashTime = 0f;
    float dashCooldownTimer;

    bool dashed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
        dashSpeed = playerShooting.CurrentWeapon().GetDashSpeed();
        dashTime = playerShooting.CurrentWeapon().GetDashTime();
        dashCooldown = playerShooting.CurrentWeapon().GetDashCooldown();
        projectileParent = GameObject.Find("Player Projectile");
    }
       
    public void Dash()
    {
        dashed = false;
        currentDashTime += Time.deltaTime;
        if (currentDashTime > dashTime)
        {
            currentDashTime = 0;
            playerMovement.SetIsDashing(false);
            dashed = true;
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
        if (dashed)
        {
            int remainingKnives = playerShooting.RemainingBulletInMag();
            float knifeAngle = knifeConeAngle / remainingKnives;
            float knife1Angle = (180 - knifeConeAngle) / 2 + dashDirection.transform.rotation.z - 90;

            for (int i = 0; i < remainingKnives; i++)
            {
                GameObject knives = Instantiate(playerShooting.CurrentWeapon().GetWeapon().gameObject, transform.position, Quaternion.identity) as GameObject;
                knives.transform.parent = knives.transform;
                Vector3 dir = new Vector3(Mathf.Cos((knife1Angle + i * knifeAngle) * Mathf.Deg2Rad), Mathf.Sin((knife1Angle + i * knifeAngle) * Mathf.Deg2Rad), 0f);
                knives.transform.up = - (Vector2)dashDirection.transform.position - (Vector2)knives.transform.position;
                knives.GetComponent<PlayerProjectile>().SetShootingDirection(dir);
                knives.GetComponent<PlayerProjectile>().SetSpeed(playerShooting.CurrentWeapon().GetProjectileSpeed());
                knives.GetComponent<PlayerProjectile>().SetFlytime(playerShooting.CurrentWeapon().GetFlyTime());
                knives.GetComponent<PlayerProjectile>().SetDamage(Mathf.RoundToInt(playerShooting.CurrentWeapon().GetDamage() * 1));
            }
            playerShooting.EmptyChamber();
        }
    }

    public void BowQuirk()
    {
        gameObject.layer = 31;
        if(dashed)
        {
            gameObject.layer = 8;
        }
    }

    public void SpearQuirk()
    {
        if (Mathf.Approximately(currentDashTime - Time.deltaTime, 0))
        {
            Instantiate(spearShadowPrefab, transform.position, Quaternion.identity);
        }
    }
}
