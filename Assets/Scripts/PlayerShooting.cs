using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon {Knives, Bow, Spears}
public class PlayerShooting : MonoBehaviour
{
    public Weapon weapon;

    [Header("Projectile")]
    [SerializeField] GameObject projectileParent;

    [Header("Weapon stats")]
    [SerializeField] WeaponProperties knives;
    [SerializeField] WeaponProperties bow;
    [SerializeField] WeaponProperties spears;

    WeaponProperties currentWeapon;

    Vector3 shootingDirection;
    PlayerProjectile weaponPrefab;

    int damage;
    int currentMag;

    float fireRate;
    float projectileSpeed;
    float currentFireTime;
    float currentDamageMultiplier = 1f;
    float reloadTime;
    float magLeft;
    float existTime;
    float flyTime;

    bool fireable = true;
    bool firing = false;

    void Start()
    {
        if (weapon == Weapon.Knives) { currentWeapon = knives; }
        else if (weapon == Weapon.Bow) { currentWeapon = bow; }
        else if (weapon == Weapon.Spears) { currentWeapon = spears; }

        magLeft = currentWeapon.GetMaxBullet();
        currentMag = currentWeapon.GetMagazine();
        weaponPrefab = currentWeapon.GetWeapon();
        projectileSpeed = currentWeapon.GetProjectileSpeed();
        existTime = currentWeapon.GetExistTime();
        reloadTime = currentWeapon.GetReloadTime();
        damage = currentWeapon.GetDamage();
        flyTime = currentWeapon.GetFlyTime();
    }

    private void Update()
    {
        if(weapon == Weapon.Spears)
        {
            HoldFire();
        }
        else if (weapon == Weapon.Bow)
        {
            BurstFire();
        }
        else if (weapon == Weapon.Knives)
        {
            RapidFire();
        }
    }

    private void BurstFire()
    {
        if (fireable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                shootingDirection = (mousePos - transform.position) / (mousePos - transform.position).magnitude;
                firing = true;
                fireable = false;
            }
        }
        if (firing)
        {
            if (currentMag > 0)
            {
                fireRate -= Time.deltaTime;
                if (fireRate <= 0)
                {
                    FireRate(currentDamageMultiplier);
                }
            }
            else
            {
                if (magLeft <= 0) { Debug.Log("No ammo"); }
                else
                {
                    firing = false;
                    FireReload();
                }
            }
        }
    }

    private void HoldFire()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log(currentDamageMultiplier);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootingDirection = (mousePos - transform.position) / (mousePos - transform.position).magnitude;
            if (currentMag > 0)
            {
                currentDamageMultiplier += Time.deltaTime;
                if (currentDamageMultiplier >= fireRate)
                {
                    currentDamageMultiplier = fireRate;
                }
            }
            else
            {
                fireable = false;
                if (magLeft <= 0) { Debug.Log("No ammo"); }
                else
                {
                    reloadTime -= Time.deltaTime;
                    if (reloadTime <= 0)
                    {
                        FireReload();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (fireable)
            {
                FireRate(currentDamageMultiplier);
            }
        }        
    }

    private void RapidFire()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootingDirection = (mousePos - transform.position) / (mousePos - transform.position).magnitude;
            if (currentMag > 0)
            {
                fireRate -= Time.deltaTime;
                if (fireRate <= 0)
                {
                    FireRate(currentDamageMultiplier);
                }
            }
            else
            {
                if (magLeft <= 0) { Debug.Log("No ammo"); }
                else
                {
                    reloadTime -= Time.deltaTime;
                    if (reloadTime <= 0)
                    {
                        FireReload();
                    }
                }
            }
        }
    }

    private void FireReload()
    {
        currentMag = currentWeapon.GetMagazine();
        magLeft -= currentWeapon.GetMagazine();
        reloadTime = currentWeapon.GetReloadTime();
        fireable = true;
    }

    private void FireRate(float currentDamageMultiplier)
    {
        Debug.Log(shootingDirection);
        GameObject playerProjectile = Instantiate(weaponPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
        playerProjectile.transform.up = (Vector2) playerProjectile.transform.position - (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerProjectile.transform.parent = projectileParent.transform;
        playerProjectile.GetComponent<PlayerProjectile>().SetShootingDirection(shootingDirection);
        playerProjectile.GetComponent<PlayerProjectile>().SetSpeed(projectileSpeed);
        playerProjectile.GetComponent<PlayerProjectile>().SetFlytime(flyTime);
        playerProjectile.GetComponent<PlayerProjectile>().SetDamage(Mathf.RoundToInt(damage * currentDamageMultiplier));
        Destroy(playerProjectile, existTime);
        fireRate = currentWeapon.GetFireRate();
        currentDamageMultiplier = 1;
        currentMag -= 1;
    }

    public WeaponProperties CurrentWeapon() { return currentWeapon; }
    public int RemainingBulletInMag() { return currentWeapon.GetMagazine() - currentMag; }
    public void PickUpBullet(int bulletPickedUp) { magLeft += bulletPickedUp; }
}
