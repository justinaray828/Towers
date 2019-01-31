using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTowerGun : MonoBehaviour {

    private Delay shootingDelay;
    [SerializeField] float shootingRate = 1f;
    [SerializeField] float shootingSpeed = 1f;
    [SerializeField] int shootingDamage= 1;
    private Quaternion gunRotation;

    private GameObject player;
    private GameObject target;
    private bool lockOnEnemy = false;
    private PooledShooting pooledShooting;

    void Start ()
    {
        shootingDelay = new Delay(shootingRate);
        player = GameObject.FindGameObjectWithTag("Player");
        pooledShooting = new PooledShooting();
	}

    private void Update()
    {

        if (!target)
        {
            lockOnEnemy = false;
        }

        if (lockOnEnemy)
        {
            gunRotation = Utilities.QuaternionBetweenTwoPoints(transform.position, target.transform.position);
            transform.rotation = gunRotation;
            shoot();
        }

    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Enemy" && !lockOnEnemy )
        {
            target = coll.gameObject;
            lockOnEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject == target)
        {
            lockOnEnemy = false;
        }
    }

    private void shoot()
    {
        if (shootingDelay.CallDelay())
        {
            pooledShooting.ShootProjectile(pooledShooting.dart, shootingDamage, shootingSpeed, transform.position, gunRotation);
        }
    }

}
