using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTowerGun : MonoBehaviour {

    private DelayBool shootingDelay;
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
        shootingDelay = new DelayBool(shootingRate);
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
        if (CallDelay(shootingDelay))
        {
            pooledShooting.ShootProjectile(pooledShooting.dart, shootingDamage, shootingSpeed, transform.position, gunRotation);
        }
    }


    //DelayBool Timer
    //--------------------------------------------------------
    private IEnumerator Delay(DelayBool delayBool)
    {
        delayBool.delayBoolState = false;
        yield return new WaitForSeconds(delayBool.delayTime);
        delayBool.delayBoolState = true;
    }

    /// <summary>
    /// Starts Delay and returns true if the delay starts and false if the delay is in progress
    /// </summary>
    /// <param name="delayBool"></param>
    /// <returns></returns>
    private bool CallDelay(DelayBool delayBool)
    {
        if (delayBool.delayBoolState == true)
        {
            StartCoroutine(Delay(delayBool));
            return true;
        }
        else
        {
            return false;
        }
    }
    //--------------------------------------------------------
    //
}
