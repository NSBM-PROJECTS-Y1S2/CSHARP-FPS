using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private ObjectPooler objectPooler;
    public Camera playerCamera;

    //shooting
    public bool isShooting, readyToShoot;
    bool  allowReset = true;
    // bool readyToShoot = true; //Debug
    public float shootingDelay = 2f;

    //Burst Mode
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    // Spread
    public float spreadIntensity;

    // Bullet Properties
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletLife = 3f;

    public enum ShootingMode 
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;


    private void Awake() 
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     FireWeapon();
        // }
        if (currentShootingMode == ShootingMode.Auto) {
            // Holding Down Left Mouse
            isShooting = Input.GetKey(KeyCode.Mouse0);
        } else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst) {
            // Pressing Left Mouse once
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        if (isShooting && readyToShoot) {
            // if readyToShoot is false, it will not run so to make readyToShoot true it must be true. Doesnt make sense to me
            //readyToShoot = true; 
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        readyToShoot = false;

        Vector3 shootDirection = CalculateDirectionAndSpread().normalized;

        //Init bullet
        //GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        GameObject bullet = objectPooler.SpawnFromPool("Bullet", bulletSpawn.position, Quaternion.identity);
        //Pointing At the shooting direction
        bullet.transform.forward = shootDirection;
        //shoot
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        //destroy after sometime
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLife));

        // Check if we are done shooting
        if (allowReset == true) {
            Invoke("ResetShot",shootingDelay);
            allowReset = false;
        }


        // Burst Mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void ResetShot () 
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread() 
    {
        // Shooting from the middle of the screen to check where are we pointing at
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.05f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit)) {
            // Hitting Something
            targetPoint = hit.point;
        } else {
            // Shooting at air
            targetPoint = ray.GetPoint(1000);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        // Returning Shooting Direction and spread
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float _bulletLife)
    {
        yield return new WaitForSeconds(_bulletLife);
        if (bullet != null)
        {
            //Destroy(bullet);
            objectPooler.ReturnToPool("Bullet", bullet);
        }
    }

}
