using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Configs & Components")]
    public int slimeHleath = 30;
    private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private float speed = 5;
    [SerializeField] private int Enemydamage = 10;
    [SerializeField] private float detectionRange;
    [SerializeField] private bool isFollowingPlayer;
    [SerializeField] Transform playerTransform;
    [SerializeField] private float enemyStopDistance;
    [SerializeField] private bool playerDeteced;
    private Collider2D slimeCollider;
    private Animator slimeAnimator;
    private Rigidbody2D rb;
    private HealthSystem healthSystem;

    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        rb = GetComponent<Rigidbody2D>();
        slimeAnimator = GetComponent<Animator>();
        slimeCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SlimeMovement();
        SlimeDeath();
        EnemyFlip();
    }

    private void SlimeDeath()
    {
        if (slimeHleath <= 0)
        {//if health rached 0 stop the enemy and play death animations
            slimeCollider.isTrigger = true;
            slimeAnimator.SetTrigger("SlimeKilled");
            rb.velocity = Vector2.zero;
        }
    }


    IEnumerator HitFlashing()
    {
        enemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        enemySpriteRenderer.color = Color.white;
    }

    private void SlimeMovement()
    {//distance betweem player and the enemy
       

        if (healthSystem.currentHealth <= 0)
            return;

        float distance = Vector2.Distance(playerTransform.position, transform.position);

        if (distance < detectionRange)
        {//if player in detectionRange then start follow the player
            isFollowingPlayer = true;
        }
        else 
        {//else stop
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer)
        {//apply velocity and start move animations
            slimeAnimator.SetBool("SlimeMove", true);
            Vector2 direction = playerTransform.position - transform.position;
            rb.velocity = direction.normalized * speed;
        }
        else
        {//else stop and ston the animations
            slimeAnimator.SetBool("SlimeMove", false);
            rb.velocity = Vector2.zero;
        }

        //check if the enemy is standing above the player
        if (transform.position.y > playerTransform.position.y + 1)
        {
            float enemyYvelocity = rb.velocity.y;
            //move the enemy down
            enemyYvelocity = -10;
        }
    }

    private void EnemyFlip()
    {//fliping the enemy by rotate it on the y axis
        if (rb.velocity.x < 0)
            transform.eulerAngles = new Vector2(0, 180);
        else if (rb.velocity.x > 0)
            transform.eulerAngles = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {//dmg the player if touched
            healthSystem.ApplyDamage(Enemydamage);
        }
    }
}
