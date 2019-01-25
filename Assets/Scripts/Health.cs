using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles health
/// </summary>
public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float healthBarYPosition = .75f;
    private int currentHealth;
    [SerializeField] private GameObject healthCanvas;
    private Slider healthSlider;

    private void Start()
    {
        healthCanvas = Instantiate(healthCanvas);
        UpdateHealthCanvasPosition();
        healthSlider = healthCanvas.transform.GetChild(0).GetComponent<Slider>();

        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        UpdateHealthCanvasPosition();
    }

    private void UpdateHealthCanvasPosition()
    {
        healthCanvas.transform.position = new Vector3(transform.position.x, transform.position.y + healthBarYPosition, 0);
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
        if (currentHealth < maxHealth)
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
            Destroy(healthCanvas);
            Destroy(gameObject);
        }
    }
}


