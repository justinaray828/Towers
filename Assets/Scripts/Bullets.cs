using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles everything with enemy bullets
/// </summary>
public class Bullets : MonoBehaviour {

    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletSpinSpeed = 5f;
    [SerializeField] private float spinDuration = 12f;
    [SerializeField] private int bulletDamage = 1;

    private Quaternion angle = Quaternion.AngleAxis(0, Vector3.forward);
    private float angleDegree = 0;
    private DelayBool bulletSpinDelay;
    private float start = 0;
    private Transform player;
    private PooledShooting pooledShooting;

    private void Start()
    {
        bulletSpinDelay = new DelayBool();
        bulletSpinDelay.delayTime = bulletSpinSpeed;
        FindPlayer();
        pooledShooting = new PooledShooting();
    }

    private void FindPlayer()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        catch
        {
            Debug.Log("Player is not in scene");
        }
    }

    private void SpawnBullet(Quaternion angle, Transform position, float speed)
    {
        speed = speed * Time.deltaTime;

        GameObject Laser = ObjectPooler.SharedInstance.GetPooledObject("Laser");
        Laser.GetComponent<Projectile>().SetProjectileSpeed(bulletSpeed);
        Laser.transform.position = gameObject.transform.position;
        Laser.transform.rotation = angle;
        Laser.SetActive(true);
    }

    public void ShootPlayer()
    {
        //// Get Angle in Radians
        //float AngleRad = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x);
        //// Get Angle in Degrees
        //float ang = (180 / Mathf.PI) * AngleRad;

        //angle = Quaternion.AngleAxis(ang, Vector3.forward);

        //SpawnBullet(angle, transform, bulletSpeed);

        angle = Utilities.QuaternionBetweenTwoPoints(transform.position, player.position);
        pooledShooting.ShootProjectile(pooledShooting.laser, bulletDamage, bulletSpeed, transform.position, angle);
    }

    public void BulletCircle(float numberOfBullets)
    {
        angleDegree = 360 / numberOfBullets;
        float tempAngle = 0;

        while(numberOfBullets > 0)
        {
            angle = Quaternion.AngleAxis(tempAngle, Vector3.forward);
            SpawnBullet(angle, transform, bulletSpeed);
            tempAngle += angleDegree;
            numberOfBullets--;
        }
    }

    private void BulletSpin()
    {

        if( (start < spinDuration) && bulletSpinDelay.boolean)
        {
            start++;

            angleDegree += 30;
            angle = Quaternion.AngleAxis(angleDegree, Vector3.forward);

            SpawnBullet(angle, gameObject.transform, bulletSpeed);
            StartCoroutine(Delay(bulletSpinDelay));
        }

    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    private IEnumerator Delay(DelayBool delayBool)
    {
        delayBool.boolean = false;
        yield return new WaitForSeconds(delayBool.delayTime);
        delayBool.boolean = true;
    }


    private class DelayBool
    {
        public bool boolean = true;
        public float delayTime;
    }

    public void Test()
    {
        ShootPlayer();
    }
}
