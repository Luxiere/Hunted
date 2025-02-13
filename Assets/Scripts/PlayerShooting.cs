﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon {Knives, Bow, Spears}
public class PlayerShooting : MonoBehaviour
{
    public static Weapon weapon;

    [Header("Projectile")]
    [SerializeField] GameObject projectileParent;

    [Header("Weapon stats")]
    [SerializeField] WeaponProperties knives;
    [SerializeField] WeaponProperties arrows;
    [SerializeField] WeaponProperties spears;

    [Header("Weapon hand")]
    [SerializeField] SpriteRenderer hand;
    [SerializeField] Sprite knife;
    [SerializeField] Sprite bow;
    [SerializeField] Sprite spear;

    [Header("SFX")]
    [SerializeField] AudioClip knifeSound;
    [SerializeField] AudioClip bowSound;
    [SerializeField] AudioClip arrowSound;
    [SerializeField] AudioClip noAmmo;

    WeaponProperties currentWeapon;

    Animator animator;

    Vector3 shootingDirection;
    PlayerProjectile weaponPrefab;

    int damage;
    int currentMag;
    int magLeft;

    float fireRate;
    float projectileSpeed;
    float currentFireTime;
    float currentDamageMultiplier = 1f;
    float reloadTime;
    float existTime;
    float flyTime;

    bool UI_On = true;
    bool fireable = true;
    bool firing = false;
        
    private void Awake()
    {
        switch (weapon)
        {
            case Weapon.Knives:
                currentWeapon = knives;
                hand.sprite = knife;
                break;
            case Weapon.Bow:
                currentWeapon = arrows;
                hand.sprite = bow;
                break;
            case Weapon.Spears:
                currentWeapon = spears;
                hand.sprite = spear;
                break;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (!UI_On)
        {
            switch (weapon)
            {
                case Weapon.Knives:
                    RapidFire();
                    break;
                case Weapon.Bow:
                    BurstFire();
                    break;
                case Weapon.Spears:
                    HoldFire();
                    break;
            }
        }
    }

    private void BurstFire()
    {
        if (fireable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                shootingDirection = (mousePos - (Vector2) transform.position) / (mousePos - (Vector2) transform.position).magnitude;
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
                    AudioSource.PlayClipAtPoint(bowSound, Camera.main.transform.position, PlayerPrefsController.GetSoundVolume());
                    ResetFireRate();
                }
            }
            else
            {
                hand.enabled = false;
                if (magLeft <= 0) { AudioSource.PlayClipAtPoint(noAmmo, Camera.main.transform.position, PlayerPrefsController.GetSoundVolume()); firing = false; }
                else
                {
                    hand.enabled = true;
                    firing = false;
                    FireReload();
                }
            }
        }
    }

    private void HoldFire()
    {
        if (currentMag > 0)
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetBool("isHolding", true);
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                shootingDirection = (mousePos - (Vector2)transform.position) / (mousePos - (Vector2)transform.position).magnitude;

                currentDamageMultiplier += Time.deltaTime;
                if (currentDamageMultiplier >= fireRate)
                {
                    currentDamageMultiplier = fireRate;
                }
            }         
            else if (Input.GetMouseButtonUp(0))
            {
                if (fireable)
                {
                    animator.SetBool("isHolding", false);
                    FireRate(currentDamageMultiplier);
                    AudioSource.PlayClipAtPoint(bowSound, Camera.main.transform.position, PlayerPrefsController.GetSoundVolume());
                    ResetFireRate();
                }
            }
        }
        else
        {
            animator.SetBool("isHolding", false);
            fireable = false;
            if (magLeft <= 0) { if (Input.GetMouseButtonDown(0)) { AudioSource.PlayClipAtPoint(noAmmo, Camera.main.transform.position, PlayerPrefsController.GetSoundVolume()); } }
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

    private void RapidFire()
    {
        if (currentMag > 0)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootingDirection = (mousePos - (Vector2)transform.position) / (mousePos - (Vector2)transform.position).magnitude;
            if (Input.GetMouseButton(0))
            {
                fireRate -= Time.deltaTime;
                if (fireRate <= 0)
                {
                    FireRate(currentDamageMultiplier);
                    AudioSource.PlayClipAtPoint(bowSound, Camera.main.transform.position, PlayerPrefsController.GetSoundVolume());
                    ResetFireRate();
                }
            }
        }
        else
        {
            hand.enabled = false;
            if (magLeft <= 0) { if (Input.GetMouseButtonDown(0)) { AudioSource.PlayClipAtPoint(noAmmo, Camera.main.transform.position, PlayerPrefsController.GetSoundVolume()); } }
            else
            {
                reloadTime -= Time.deltaTime;
                if (reloadTime <= 0)
                {
                    FireReload();
                    hand.enabled = true;
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

    public void FireRate(float currentDamageMultiplier)
    {
        GameObject playerProjectile = Instantiate(weaponPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
        playerProjectile.transform.up = (Vector2) playerProjectile.transform.position - (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerProjectile.transform.parent = projectileParent.transform;
        playerProjectile.GetComponent<PlayerProjectile>().SetShootingDirection(shootingDirection);
        playerProjectile.GetComponent<PlayerProjectile>().SetSpeed(projectileSpeed);
        playerProjectile.GetComponent<PlayerProjectile>().SetFlytime(flyTime);
        playerProjectile.GetComponent<PlayerProjectile>().SetDamage(Mathf.RoundToInt(damage * currentDamageMultiplier));
        Destroy(playerProjectile, existTime);
    }

    private void ResetFireRate()
    {
        fireRate = currentWeapon.GetFireRate();
        this.currentDamageMultiplier = 1;
        currentMag -= 1;
    }

    public WeaponProperties CurrentWeapon() { return currentWeapon; }
    public int RemainingBulletInMag() { return currentMag; }
    public int GetMaxMag() { return magLeft; }
    public void EmptyChamber() { currentMag = 0; }
    public void PickUpBullet(int bulletPickedUp) { magLeft += bulletPickedUp; }
    public void UIOnOff(bool uiOn) { UI_On = uiOn; }
    public static void SetWeapon(Weapon Weapon) { weapon = Weapon; }
}
