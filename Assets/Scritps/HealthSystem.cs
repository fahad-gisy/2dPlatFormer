using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float damageStunTime;
    public float currentHealth;
    private PlayerController playerController;

    [SerializeField] private Image healthBar;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        SoundManager.instance.PlayPlayerDamagedSound();
        UpdateHealth();
        if (!playerController.IsDead())
        {
            StartCoroutine(DamageStun());
        }
        PlayerKilled();
    }

    IEnumerator DamageStun()
    {
        playerController.animator.Play("Damaged");
        playerController.enabled = false;
        yield return new WaitForSeconds(damageStunTime);
        playerController.enabled = true;
        playerController.animator.CrossFade("idle", 0);
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
