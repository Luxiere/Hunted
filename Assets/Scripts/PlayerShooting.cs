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

    float fireRate;
    float projectileSpeed;
    float currentFireTime;
    float currentDamageMultiplier = 1f;
    float reloadTime;
    float magLeft;
    float currentMag;
    float existTime;
    float flyTime;

    bool fireable = true;

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
        fireRate = currentWeapon.GetFireRate();
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
        bool firing = false;
        if (fireable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                shootingDirection = (mousePos - transform.position) / (mousePos - transform.position).magnitude;
                firing = true;
            }
        }
        if (firing)
        {
            if (currentMag > 0)
            {
                StartCoroutine(FireRate(fireRate, currentDamageMultiplier));
            }
            else
            {
                fireable = false;
                if (magLeft <= 0) { Debug.Log("No ammo"); }
                else
                {
                    firing = false;
                    StartCoroutine(FireReload(reloadTime));
                }
            }
        }
    }

    private IEnumerator FireReload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        currentMag = currentWeapon.GetMagazine();
        magLeft -= currentWeapon.GetMagazine();
        this.reloadTime = currentWeapon.GetReloadTime();
        fireable = true;
    }

    private IEnumerator FireRate(float fireRate, float currentDamageMultiplier)
    {
        GameObject playerProjectile = Instantiate(weaponPrefab.gameObject, transform.position, Quaternion.Euler(shootingDirection)) as GameObject;
        playerProjectile.transform.parent = projectileParent.transform;
        playerProjectile.GetComponent<PlayerProjectile>().SetShootingDirection(shootingDirection);
        playerProjectile.GetComponent<PlayerProjectile>().SetSpeed(projectileSpeed);
        playerProjectile.GetComponent<PlayerProjectile>().SetFlytime(flyTime);
        playerProjectile.GetComponent<PlayerProjectile>().SetDamage(Mathf.RoundToInt(damage * currentDamageMultiplier));
        Destroy(playerProjectile, existTime);
        currentDamageMultiplier = 1;
        currentMag -= 1;
        yield return new WaitForSeconds(fireRate);
    }

    private void HoldFire()
    {
        if (Input.GetMouseButton(0))
        {
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
                        StartCoroutine(FireReload(0f));
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (fireable)
            {
                StartCoroutine(FireRate(0f, currentDamageMultiplier));
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
                currentFireTime -= Time.deltaTime;
                if (currentFireTime <= 0)
                {
                    StartCoroutine(FireRate(0f, currentDamageMultiplier));
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
                        StartCoroutine(FireReload(0f));
                    }
                }
            }
        }
    }

    public WeaponProperties CurrentWeapon() { return currentWeapon; }
    public void PickUpBullet(int bulletPickedUp) { magLeft += bulletPickedUp; }
}
