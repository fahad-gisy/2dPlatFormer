using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;//player's max health
    [SerializeField] private float damageStunTime;//stun time if the player get damaged
    public float currentHealth;
    private PlayerController playerController;//player's main script

    [SerializeField] private Image healthBar;//image for health bar

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();//player main script component
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;//deal dmg by dmg - health
        SoundManager.instance.PlayPlayerDamagedSound();//dmg sound
        UpdateHealth();//update health after taking dmg
        if (!playerController.IsDead())
        {
            StartCoroutine(DamageStun());// if player not dead then play the stun animation
        }
        PlayerKilled();//check if the player dead yet
    }

    IEnumerator DamageStun()
    {
        playerController.animator.Play("Damaged");//dmg stun anim
        playerController.enabled = false;//stop the player main script so he can't do anything while stun
        yield return new WaitForSeconds(damageStunTime);//stun time
        //then player can controll again and the dmg anim stop
        playerController.enabled = true;
        playerController.animator.CrossFade("idle", 0);
    }
    public void PlayerKilled()
    {
        if (currentHealth <= 0)
        {
            //if the palyer dead after 2 sec load lose scene
            StartCoroutine(WaitToLoadScene());
        }
    }

    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LoseScene");
    }

    private void UpdateHealth()
    {//update health Ui after taking dmg
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
