using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    static float xside = 30f;
    static float yside = 10f;
    static float xtop = 45f;
    static float ytop = 20f;

    [SerializeField] float runSpeed;
    [SerializeField] float firstJumpSpeed = 25.0f;
    [SerializeField] float secondJumpSpeed = 10.0f;
    [SerializeField] float climbSpeed = 3.0f;
    [SerializeField] int currentHealth = 100;
    [SerializeField] Vector2 flyside = new Vector2(xside, yside);
    [SerializeField] Vector2 flytop = new Vector2(xtop, ytop);
    [SerializeField] TextMeshProUGUI HealthText;
    
    public Rigidbody2D rigidbody;
    private Animator animatorComponent;
    int jumpcount = 0;
    private SpriteRenderer spriteComponent;
    bool running = false;
    bool isAlive = true;
    bool FaceRight = false;

    CapsuleCollider2D bodyCollider;
    BoxCollider2D playerFeet;
    float previousGravityScale;


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
        WhichWayFacing();
        Jump();
        ClimbLadder();
        CheckIfTouchingGround();
        Trap();
        Enemy();
        CheckAlive();
    }

    void CheckAlive()
    {
        if(currentHealth > 0)
        {
            isAlive = false;
        }
        else
        {
            isAlive = true;
        }
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

    private void Enemy()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            healthBar.damageHealth(5);
            GetComponent<Rigidbody2D>().velocity = flyside;
            currentHealth -= 5;
        }
        else if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            healthBar.damageHealth(5);
            GetComponent<Rigidbody2D>().velocity = flytop;
            currentHealth -= 5;
        }
    }


    private void Trap() {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Traps")))
        {
            healthBar.damageHealth(5);
            currentHealth -= 5;
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
        }
    }

    private void CheckIfTouchingGround()
    {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumpcount = 0;
        }
    }

    void WhichWayFacing()
    {
        if (FaceRight)
        {
            flyside = new Vector2(-xside, yside);
            flytop = new Vector2(-xtop, ytop);
        }
        else
        {
            flyside = new Vector2(xside, yside);
            flytop = new Vector2(xtop, ytop);
        }
    }

    private void HandleHorizontalMovement()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        var velocity = rigidbody.velocity;
        
        if (direction > 0f)
        {
            spriteComponent.flipX = false; // Faces right
            FaceRight = true;
        }
        else if (direction < 0f)
        {
            spriteComponent.flipX = true; // Faces left
            FaceRight = false;
        }


        var position = transform.position; //Qipa kullanmak istemiş, kullanmamış. Deniz canimsin <3

        animatorComponent.SetBool(10, Mathf.Abs(direction) > 0.0f);
    }
}
