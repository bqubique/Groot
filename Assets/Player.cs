using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 1.0f;
    public Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }
    
    void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, rigidbody.velocity.y);
        rigidbody.velocity = playerVelocity;
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("Space key was pressed.");
            for(int i= 0; i<10; i++)
            {
                Debug.Log("whatever");
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space key was released.");
        }

    }

    void FlipSprite()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {

            //transform.localScale = new Vector2(Mathf.Sign(rigidbody.velocity.x), transform.localScale.y);
        }
    }
}
