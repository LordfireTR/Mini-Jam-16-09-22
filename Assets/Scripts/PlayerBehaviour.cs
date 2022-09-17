using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed, _moveSpeed, jumpSpeed, gravityMultiplier, inputHorizontal, dragGround, dragAir, screenLimitLeft;
    [SerializeField] BackgroundManager BackgroundManager;
    public float screenLimitRight;

    Rigidbody2D rb;
    public bool isGrounded, facingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _moveSpeed = 5;
        jumpSpeed = 12;
        gravityMultiplier = 2.5f;
        dragGround = 4;
        dragAir = 1;
        screenLimitLeft = -16;
        screenLimitRight = 10f;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovementHandler();
        ScreenBorders();
        JumpHandler();
        FallHandler();
    }

    void MovementHandler()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        
        rb.AddForce(inputHorizontal * Vector3.right);

        if (inputHorizontal > 0 && !facingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        else if (inputHorizontal < 0 && facingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
    }

    void ScreenBorders()
    {
        if(transform.position.x < screenLimitLeft)
        {
            transform.position = new Vector3(screenLimitLeft, transform.position.y, 0);
        }
        else if (BackgroundManager.CanMove() && transform.position.x > screenLimitRight)
        {
            transform.position = new Vector3(screenLimitRight, transform.position.y, 0);
        }
        else if (!BackgroundManager.CanMove() && transform.position.x > -screenLimitLeft)
        {
            transform.position = new Vector3(-screenLimitLeft, transform.position.y, 0);
        }
    }

    void JumpHandler()
    {
        if (isGrounded)
        {
            rb.drag = dragGround;
            moveSpeed = _moveSpeed;
            if (Input.GetKeyDown("space"))
            {
                rb.AddForce(jumpSpeed * Vector3.up, ForceMode2D.Impulse);
            }
        }
        else
        {
            rb.drag = dragAir;
            moveSpeed = _moveSpeed/5;
        }
    }

    void FallHandler()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = gravityMultiplier;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
