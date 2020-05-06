using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float firstJumpSpeed = 25.0f;
    [SerializeField] float secondJumpSpeed = 10.0f;
    [SerializeField] float climbSpeed = 3.0f;
    [SerializeField] int health = 100;
    [SerializeField] TextMeshProUGUI HealthText;

    public Rigidbody2D rigidbody;
    private Animator animatorComponent;
    int jumpcount = 0;
    private SpriteRenderer spriteComponent;
    bool running = false;

    CapsuleCollider2D bodyCollider;
    BoxCollider2D playerFeet;
    float previousGravityScale;

    public int maxHealth = 100;
    public int currentHealth;

    [SerializeField] public HealthBar healthBar;

    void Start()
    {
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(100);
        rigidbody = GetComponent<Rigidbody2D>();
        animatorComponent = GetComponent<Animator>();
        spriteComponent = GetComponent<SpriteRenderer>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        previousGravityScale = rigidbody.gravityScale;
        playerFeet = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Run();
        HandleHorizontalMovement();
        Jump();
        ClimbLadder();
        CheckIfTouchingGround();
        Trap();
        checkHealth();
    }

    void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            runSpeed = 10;
            playerVelocity = new Vector2(controlThrow * runSpeed, rigidbody.velocity.y);
            rigidbody.velocity = playerVelocity;
        }
        else
        {
            runSpeed = 6;
            playerVelocity = new Vector2(controlThrow * runSpeed, rigidbody.velocity.y);
            rigidbody.velocity = playerVelocity;
        }
    }

    private void checkHealth() {
        if (health <= 0) {
            //LOAD GAME OVER SCENE HERE 
        }
    }


    private void Trap() {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Traps")))
        {
            healthBar.damageHealth(5);
        }

    }

    private void ClimbLadder()
    {
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            animatorComponent.SetBool("climbing", false);
            rigidbody.gravityScale = previousGravityScale;
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rigidbody.velocity.x,controlThrow*climbSpeed);
        rigidbody.velocity = climbVelocity;
        rigidbody.gravityScale = 0;
        bool verticalSpeed = Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon;
        animatorComponent.SetBool("climbing", verticalSpeed);
    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if(jumpcount == 0)
            {
                Vector2 JumpVelocityToAdd = new Vector2(0f, firstJumpSpeed);
                rigidbody.velocity = JumpVelocityToAdd;
                jumpcount++;
            }
            else if (jumpcount == 1)
            {
                Vector2 JumpVelocityToAdd = new Vector2(0f, secondJumpSpeed);
                rigidbody.velocity = JumpVelocityToAdd;
                jumpcount++;
            }
            //return;
        }
    }

    private void CheckIfTouchingGround()
    {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            jumpcount = 0;
        }
    }

    private void HandleHorizontalMovement()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        var velocity = rigidbody.velocity;
        
        if (direction > 0f)
        {
            spriteComponent.flipX = false; // Faces right
        }
        else if (direction < 0f)
        {
            spriteComponent.flipX = true; // Faces left
        }


        var position = transform.position; //Qipa kullanmak istemiş, kullanmamış. Deniz canimsin <3

        animatorComponent.SetBool(10, Mathf.Abs(direction) > 0.0f);
    }
}
