using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Weapon : MonoBehaviour
{

    public bool isActiveweapon;
    // Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;


    // Burst
    public int bulletsPerBurst = 3;
    public int burstBullets Left;

    // Spread
    public float spreadIntensity;

    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f; // seconds

    public GameObject muzzleEffect;
    private Animator animator;

    // Loading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    public Vector3 spawnPosition;
    public Vector3 spawnRotation;


    public enum WeaponModel
    {
        Pistol1911,
        M16
    }

    public Weapon Model thisWeaponModel;

     public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = trur;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;
    }


    void Update()
    {

        if (isActiveweapon)
        {
            GetComponent<Outline>().enabled = false;
            // Empty Magazine Sound
            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager Instance emptyManagizeSound1911 Play();
            }


            if (currentShootingMode == ShootingMode Auto)
            {
                // Holding Down Left Mouse Button
                isShooting = Input.GetKey(KeyCode Mouse0);
            }
            else if (currentShootingMode ShootingMode Single ||
                  currentShootingMode ShootingMode Burst)
            {
                // Clicking Left Mouse Button Once
                isShooting = Input.GetKeyDown(KeyCode Mouse0);
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletsleft < magazineSize && isReloading == false)
            {
                Reload();
            }

            // If you want to automatically reload when magazine is empty
            if (readyToShoot && isShooting == false && isReloading == false && bulletsLeft <= 0)
            {
                //Reload();
            }


            if (readyToShoot && isShooting && bulletsLeft > 0)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }

            if (AmmoManager Instance ammoDisplay != null)
           {
                AmmoManager Instance ammoDisplay = text $"{bulletsLeft bulletsPerBurst}/{magazineSize bulletsPerBurst}";
            }

        }
    }

    private void FireWeapon()
    {
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play(); 
        animator SetTrigger("RECOIL");

        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;

        Vector3 shootingDirection = CulateDirectionAndSpread().normalized;
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Poiting the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        // Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        // Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        // Checking if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot",shootingDelay);
            allowReset = false;
        }

        //Burst Mode
        if (currentShootingMode == ShootingMode.Burst && burstbulletsLeft > 1)//we already shoot once before this check
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);            
        }
    }    

    private void Reload()
    {
        SoundManager.Instance.PlayReloadsound(thisWeaponModel1);

        animator.SetTrigger("RELOAD)");

        isReloading = true;
        Invoke("ReloadCompleted",reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {

    }
        
