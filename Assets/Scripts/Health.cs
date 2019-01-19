using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles health
/// </summary>
public class Health : MonoBehaviour {

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Hurt GameObject by Int Damage
    /// </summary>
    /// <param name="damage"></param>
    public void HurtGameObject(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        HealthCheck();
        ActivateHealthBar();
    }

    private void ActivateHealthBar()
    {
        if(currentHealth < maxHealth)
        {
            healthSlider.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Checks if health is 0 or below and kills enemy if true
    /// </summary>
    private void HealthCheck()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}


