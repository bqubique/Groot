using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Rigidbody2D body;
    bool FacingRight = true;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        isFacingRight();
        if (FacingRight)
        {
            body.velocity = new Vector2(speed, 0f);
        }
        else
        {
            body.velocity = new Vector2(-speed, 0f);
        }
                
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(body.velocity.x)),1f);
    }

    private void isFacingRight()
    {
        if(transform.localScale.x > 0)
        {
            FacingRight = true;
        }
        else
        {
            FacingRight = false;
        }
    }
}
