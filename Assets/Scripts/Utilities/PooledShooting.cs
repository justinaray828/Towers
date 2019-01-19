using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledShooting
{
    [Header("Projectile Prefab Name")]
    private const string DART = "Dart";
    private const string LASER = "Laser";

    public string dart;
    public string laser;

    public PooledShooting()
    {
        dart = DART;
        laser = LASER;
    }

    public void ShootProjectile(string projectilePrefab, int damage, float speed, Vector3 shootingLocation, Quaternion dartRotation)
    {
        GameObject dart = ObjectPooler.SharedInstance.GetPooledObject(projectilePrefab);

        if (dart != null)
        {
            dart.GetComponent<Projectile>().SetProjectileDamage(damage);
            dart.GetComponent<Projectile>().SetProjectileSpeed(speed);

            dart.transform.position = shootingLocation;
            dart.transform.rotation = dartRotation;
            dart.SetActive(true);
        }
    }

}
