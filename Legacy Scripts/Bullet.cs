using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int bulletDamage = 1;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<PlayerManager>().HurtPlayer(bulletDamage);
            gameObject.SetActive(false);
        }
    }
}
