using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    [SerializeField] private Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealth();
        PlayerKilled();
    }
    public void PlayerKilled()
    {
        if (currentHealth <= 0)
        {
            // Die
        }
    }
    public void Heal(float healing)
    {
        currentHealth += healing;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
