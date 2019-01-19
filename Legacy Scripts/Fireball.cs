using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle fireball information
/// </summary>
public class Fireball : MonoBehaviour {

    private float fireballDamage;

    /// <summary>
    /// Set amount of damage the fireball will do
    /// </summary>
    /// <param name="damage"></param>
    public void SetFireballDamage(float damage)
    {
        fireballDamage = damage;
    }

    /// <summary>
    /// Get amount of damage the fireball will do
    /// </summary>
    /// <returns>
    /// The amount of damage
    /// </returns>
    public float GetFireballDamage()
    {
        return fireballDamage;
    }
}
