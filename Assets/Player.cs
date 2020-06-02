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
    [SerializeField] public HealthBar healthBar;
    [SerializeField] TextMeshProUGUI HealthText;
    [SerializeField] GameObject deathCanvas;
    [SerializeField] GameObject pauseCanvas;

    CapsuleCollider2D bodyCollider;
    BoxCollider2D playerFeet;

    public Rigidbody2D rigidbody;
    private Animator animatorComponent;
    private SpriteRenderer spriteComponent;
    private Animation animation;
    private Camera camera1;
    private Camera camera2;
    private Camera camera3;
    int jumpcount = 0;
    bool running = false;
    bool FaceRight = false;
    float previousGravityScale;

    void Start()
    {
        Time.timeScale = 1f;
        deathCanvas.SetActive(false);
        GameObject.Find("NextLevelPortal").transform.localScale = new Vector3(0, 0, 0);
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(100);
        rigidbody = GetComponent<Rigidbody2D>();
        animatorComponent = GetComponent<Animator>();
        spriteComponent = GetComponent<SpriteRenderer>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        previousGravityScale = rigidbody.gravityScale;
        playerFeet = GetComponent<BoxCollider2D>();
        camera1 = GameObject.Find("Main Camera").GetComponent<Camera>();
        camera2 = GameObject.Find("Map1").GetComponent<Camera>();
        camera3 = GameObject.Find("Map2").GetComponent<Camera>();
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
    }

    void Update()
    {
        SwitchCamera();
        Run();
        HandleHorizontalMovement();
        WhichWayFacing();
        Jump();
        ClimbLadder();
        CheckIfTouchingGround();
        Trap();
        Enemy();
        CheckAlive();
        CheckLiquid();
        CheckGameStatus();
        if (currentHealth <= 0)
        {
            deathCanvas.SetActive(true);
            pauseCanvas.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    void CheckGameStatus()
    {
        if(GameObject.Find("CoinLayer").transform.childCount == 0)
        {
            Debug.Log("Portal has been opened!");
            GameObject.Find("NextLevelPortal").transform.localScale = new Vector3(1,1,1);
        }
    }
    void CheckAlive()
    {
        if (currentHealth == 0)
        {
            animatorComponent.SetBool("isAlive", false);
            
            //Time.timeScale = 0f;
        }
    }    

    void SwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            camera1.enabled = false;
            camera2.enabled = true;
            camera3.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            camera1.enabled = true;
            camera2.enabled = false;
            camera3.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            camera1.enabled = true;
            camera2.enabled = false;
            camera3.enabled = false;
        }
    }

    void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity;
        bool verticalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        animatorComponent.SetBool("Walking", verticalSpeed);
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

    private void CheckLiquid()
    {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Liquid")))
        {
            healthBar.damageHealth(currentHealth);
            currentHealth -= 5;
        }
    }

   

    private void Trap()
    {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Traps")))
        {
            healthBar.damageHealth(5);
            currentHealth -= 5;
        }
    }

    private void ClimbLadder()
    {
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            animatorComponent.SetBool("climbing", false);      //bir ara bak
            rigidbody.gravityScale = previousGravityScale;
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rigidbody.velocity.x, controlThrow * climbSpeed);
        rigidbody.velocity = climbVelocity;
        rigidbody.gravityScale = 0;        
    }

    private void Jump()
    {
        bool horizontalSpeed = Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon;
        animatorComponent.SetBool("Jumping", horizontalSpeed);
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (jumpcount == 0)
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
            flytop = new Vector2(xtop, ytop);
        }
        else
        {
            flyside = new Vector2(xside, yside);
            flytop = new Vector2(-xtop, ytop);
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