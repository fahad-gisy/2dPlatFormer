using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Configs & Components")]
    public int slimeHleath = 30;//enemy health amount
    private bool IsFacingRight;//checking if the enemy facing right or left
    [SerializeField] private float speed = 5;//enemy movement speed
    [SerializeField] private int Enemydamage = 10;//enemy dmg
    [SerializeField] private float detectionRange;//detection range, it is like the vision range of this enemy
    [SerializeField] private bool isFollowingPlayer;//can the enemy follow the player or not
    [SerializeField] Transform playerTransform;//player transform so the enemy can know where is the player
    [SerializeField] private bool playerDetected;//player detected or not?
    private Collider2D slimeCollider;//enemy's collider
    private Animator slimeAnimator;//enemy's animator
    private Rigidbody2D rb;//enemy's rigidBody
    private float enemyYvelocity;//enemy's Y velocity
    private HealthSystem healthSystem;//health system component

    void Start()
    {
        //assgin random health & speed for the enemies
        slimeHleath = Random.Range(30,60);
        speed = Random.Range(7, 10);

        //getting all the components we need 
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
            slimeAnimator.SetTrigger("SlimeKilled");//play death Animation
            rb.velocity = Vector2.zero;//stop moving
            StartCoroutine(EnemyFaillDown());//enemy will faill to the ground after sec
            Destroy(gameObject, 10);//destroy it 
        }
    }

    IEnumerator EnemyFaillDown()
    {
        yield return new WaitForSeconds(2f);
        slimeCollider.isTrigger = true;//change the collider to trigger so it will faill down
    }
 

    private void SlimeMovement()
    {//distance betweem player and the enemy
       

        if (healthSystem.currentHealth <= 0) // if the enemy's 0 mean it dead so don't move
            return;

        float distance = Vector2.Distance(playerTransform.position, transform.position);//calculate the distance between the player & the enemy

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
            enemyYvelocity = rb.velocity.y;
            //move the enemy downs
            enemyYvelocity = -10;
        }
    }

    private void EnemyFlip()
    {//fliping the enemy by rotate it on the y axis
        if (rb.velocity.x < 0)
        {
            IsFacingRight = false;
            transform.eulerAngles = new Vector2(0, 180);
        }
            
        else if (rb.velocity.x > 0)
        {
            IsFacingRight = true;
            transform.eulerAngles = new Vector2(0, 0);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {//dmg the player if touched
            healthSystem.ApplyDamage(Enemydamage);

            if (IsFacingRight)
            {//apply velocity to the player to push him a way from enemy
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(15, 0);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-15,0);
            }
        }
    }
}
