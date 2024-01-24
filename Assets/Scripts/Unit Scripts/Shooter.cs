using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Unit
{

    // Serialize Field
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float fireRate = 2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 1, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (bulletSpawnPoint.forward * 10 );
        bullet.AddComponent<Bullet>();
    }

}
