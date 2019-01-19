using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle moving projectiles
/// </summary>
public class Projectile : MonoBehaviour {

    private enum Tag { Enemy , Player , PunchTower , GlueTower , WallTower , ShootingTower }

    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;
    [SerializeField] private Tag[] damageTag;

    /// <summary>
    /// Handles collision with trigger
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Background")
        {
            gameObject.SetActive(false);
        }

        foreach(Tag tag in damageTag)
        {
            //Player handles damage differently
            if (coll.tag == tag.ToString() && tag == Tag.Player) 
            {
                coll.GetComponent<PlayerManager>().HurtPlayer(projectileDamage);
                gameObject.SetActive(false);
            }

            if(coll.tag == tag.ToString())
            {
                coll.GetComponent<Health>().HurtGameObject(projectileDamage);
                gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * projectileSpeed;
    }

    public int GetProjectileDamage()
    {
        return projectileDamage;
    }

    /// <summary>
    /// Sets speed of projectile
    /// </summary>
    /// <param name="speed"></param>
    public void SetProjectileSpeed(float speed)
    {
        projectileSpeed = speed;
    }

    /// <summary>
    /// Sets projectile damage
    /// </summary>
    /// <param name="speed"></param>
    public void SetProjectileDamage(int damage)
    {
        projectileDamage = damage;
    }
}
