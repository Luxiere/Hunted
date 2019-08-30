using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponProperties : ScriptableObject
{
    [Header("Dash properties")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashTime = .25f;
    [SerializeField] float dashCooldown = 2f;
    [Header("Projectile properties")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField][Tooltip("Max holding time for spear, time between shots for other weapons")] float fireRate = .25f;
    [SerializeField] float existTime = 4f;
    [SerializeField] float flyingTime = 1f;
    [SerializeField] int damage = 10;
    [Header("Weapon properties")]
    [SerializeField] float reloadTime = 1f;
    [SerializeField] int maxBullet = 45;
    [SerializeField] int magazine = 15;
    [SerializeField] PlayerProjectile weapon;

    public float GetDashTime() { return dashTime; }
    public float GetDashCooldown() { return dashCooldown; }
    public float GetDashSpeed() { return dashSpeed; }

    public float GetExistTime() { return existTime; }
    public float GetProjectileSpeed() { return projectileSpeed; }
    public float GetReloadTime() { return reloadTime; }
    public float GetFireRate() { return fireRate; }
    public float GetFlyTime() { return flyingTime; }

    public int GetMagazine() { return magazine; }
    public int GetMaxBullet() { return maxBullet; }
    public int GetDamage() { return damage; }

    public PlayerProjectile GetWeapon() { return weapon; }

}
