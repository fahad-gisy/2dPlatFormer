using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    [Header("PlayerComponents")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    private HealthSystem healthSystem;

    

    [Header("Player Movements Variables")]
    [SerializeField] private float playerSpeed;// player target speed value
    private float moveInput;//float to store input value
    private bool isFacingRight = true;
    private GameObject bullet;
    [SerializeField] private float bulletShootForce;


    [Header("Jump Variables")]
    [SerializeField] private bool jumpInput;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private bool isGrounded;


    [Header("WallSliding Variables")]
    private bool IsWallSliding;
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayerMask;


    [Header("WallJumping")]
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower;



    [Header("Shooting Variables")]
    private bool startShootingInput;
    private bool shootInput;
   [SerializeField] private bool shootingToggle;
   [SerializeField] private Transform shootingPoint;
   [SerializeField] private GameObject bulletPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healthSystem = FindObjectOfType<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWallJumping)
        {
            Flip();
        }
        PlayerAnimatationHandler();
        InputHandler();
        OnStartShooting();
        wallSlide();
        OnJump();
        WallJump();
        IsDead();




        if (startShootingInput)
        {
            ShootingAnimation();
        }
    }

    private void FixedUpdate()
    {
        OnPlayerMove();

    }

    private void InputHandler()
    {//input manager's Horizontal input(A  = -1 or D = 1 Keyboard) > x axis
        moveInput = Input.GetAxis("Horizontal");

         // prese F to Shoot
        startShootingInput = Input.GetKeyDown(KeyCode.F);

        //mouse click to shoot
        shootInput = Input.GetKeyDown(KeyCode.Mouse0);

        //space for jump
        jumpInput = Input.GetKeyDown(KeyCode.Space);

    }
    //when ever player pressed A or D apply new velocity > move
    private void OnPlayerMove()
    {
        //move the player on x axis whenever A or D pressed multiple by speed so i can control player's speed
        if (!IsWallSliding && !IsDead())
        {
            rb.velocity = new Vector2(moveInput * playerSpeed * Time.deltaTime, rb.velocity.y);
        } 
    }

    private bool IsGrounded()
    {// if the player on the ground return true 
        return isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayerMask);
    }

    private void OnJump()
    {//if the player grounded then jump input pressed JUMP
        if (IsGrounded() && jumpInput && !IsDead())
        {  //if player gorouned then preseed space apply velocity on y axis to jump
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            SoundManager.instance.PlayJumpSound();
            Debug.DrawLine(transform.position, groundCheckTransform.position, IsGrounded() ? Color.green : Color.red);// drwa line for debuging
        }
    }

   
    private bool IsWalled()
    {//cheking if the player touched the wall
        return Physics2D.OverlapCircle(wallCheck.position,0.2f,wallLayerMask);
    }

    private void wallSlide()
    {
        if (IsWalled() && !IsGrounded() && moveInput != 0 && !shootingToggle && !IsDead())
        {//if the player on the wall apply new velocity on y axis to start sliding
            IsWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else 
        {

            IsWallSliding = false;
        }
    }
    //wall jumping mechanic
    private void WallJump()
    {

        if (!IsDead())
        {
            if (IsWallSliding)// if the player sliding on the wall
            {
                isWallJumping = false;
                wallJumpingDirection = -transform.localScale.x; // filp the player to the wall jumping direaction
                wallJumpingCounter = wallJumpingTime;//wall jumping time is 0.2 ms so the player wont flying 4 ever

                CancelInvoke(nameof(StopWallJumping));//invoke and stop the wall jump
            }
            else
            {
                wallJumpingCounter -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)// if allowed to jump and space pressed
            {
                isWallJumping = true;
                SoundManager.instance.PlayJumpSound();
                rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);//apply the jump velcoity x,y
                wallJumpingCounter = 0f;

                if (transform.localScale.x != wallJumpingDirection)//if the player not facing the wall then filp
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }

                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
        }
       
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }




    private void PlayerAnimatationHandler()
    {//if the player moved
        if (IsPlayerMoving())
        {
            //play move animation
            animator.SetBool("Moving", true);
        }
        else
        {//if not don't play move animation
            animator.SetBool("Moving", false);
        }
        //death animatoins if player's health reached 0 
        if (IsDead())
        {
            animator.SetTrigger("Death");
        }
    }
    //this method check if the player moving or not
    private bool IsPlayerMoving()
    {
        if (rb.velocity.x != 0f)//if the player start moving 
        {
            return true;//player moved
        }
        else
        {
            return false;////player standing still
        }
    }
    //handling shooting animation
    private void ShootingAnimation()
    {//if player pressed F, toggle the shoot animation on/off
        shootingToggle = !shootingToggle;

        if (shootingToggle)
        { //if shooting toggled start shoot animations
            animator.SetBool("Shooting",true);
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
    }

    private void OnStartShooting()
    {//if shoot input pressed and shoot mode on then instantiate the bullet
        if (shootInput && shootingToggle && !IsDead())
        {
            //spawn the bullet prefab
            bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            SoundManager.instance.PlayShootSound();
            if (isFacingRight) // if facing right
            {   //apply force to the bullet to the right axis
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletShootForce);
            }
            else //else applyf force to the left
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletShootForce);
            }
        }
    }


    public bool IsDead()
    {//checking if the player's health reached zere > dead
        if (healthSystem.currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Flip()
    {////flip the player > if moving left filp to the left and so on
        if (isFacingRight && moveInput < 0f || !isFacingRight && moveInput > 0f && !IsDead())
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;// local scale = -1 in 2d games mean filp the sprite to the left & +1 to the right
            transform.localScale = localScale;
        }
    }
}
