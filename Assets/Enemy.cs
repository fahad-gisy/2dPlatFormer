using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Configs & Components")]
    public int slimeHleath = 30;
    [SerializeField] private float speed = 5;
    [SerializeField] private int Enemydamage = 10;
    [SerializeField] private float detectionRange;
    [SerializeField] private bool isFollowingPlayer;
    [SerializeField] Transform playerTransform;
    [SerializeField] private bool playerDeteced;
    private Animator slimeAnimator;
    private Rigidbody2D rb;
    private Collider2D collider;
    private HealthSystem healthSystem;

    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        rb = GetComponent<Rigidbody2D>();
        slimeAnimator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SlimeMovement();
        SlimeDeath();
        EnemyFlip();
        Debug.Log(slimeHleath);

    }

    private void SlimeDeath()
    {
        if (slimeHleath <= 0)
        {
            slimeAnimator.SetTrigger("SlimeKilled");
            rb.velocity = Vector2.zero;
        }
    }

    private void SlimeMovement()
    {
        float distance = Vector2.Distance(playerTransform.position, transform.position);

        if (distance < detectionRange)
        {
            isFollowingPlayer = true;
        }
        else
        {
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer)
        {
            slimeAnimator.SetBool("SlimeMove", true);
            Vector2 direction = playerTransform.position - transform.position;
            rb.velocity = direction.normalized * speed;
        }
        else
        {
            slimeAnimator.SetBool("SlimeMove", false);
            rb.velocity = Vector2.zero;
        }
    }

    private void EnemyFlip()
    {
        if (rb.velocity.x < 0)
            transform.eulerAngles = new Vector2(0, 180);
        else if (rb.velocity.x > 0)
            transform.eulerAngles = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthSystem.ApplyDamage(Enemydamage);
        }
    }
}
