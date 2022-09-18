using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed, _moveSpeed, jumpSpeed, gravityMultiplier, inputHorizontal, dragGround, dragAir, screenLimitLeft, screenLimitBottom;
    //[SerializeField] BackgroundManager BackgroundManager;
    [SerializeField] Transform camTransform;
    public float screenLimitRight;
    float jumpBoolTimer, _jumpBoolTimer;

    Rigidbody2D rb;
    bool isGrounded, facingRight, jumpBool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _moveSpeed = 50;
        jumpSpeed = 6;
        gravityMultiplier = 2.5f;
        dragGround = 4;
        dragAir = 1;
        screenLimitLeft = -16;
        screenLimitBottom = -11;
        screenLimitRight = 10f;
        _jumpBoolTimer = 0.1f;
        facingRight = true;
    }

    void Update()
    {
        InputHandler();
        ScreenBorders();
    }

    void FixedUpdate()
    {
        MovementHandler(inputHorizontal);
        JumpHandler();
        FallHandler();
    }

    void InputHandler()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        if (Input.GetKeyDown("space"))
        {
            jumpBool = true;
            jumpBoolTimer = _jumpBoolTimer;
        }
        if (jumpBoolTimer > 0)
        {
            jumpBoolTimer -= Time.deltaTime;
        }
        else
        {
            jumpBool = false;
        }
    }

    void MovementHandler(float speedHorizontal)
    {
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
        if(transform.position.x < camTransform.position.x + screenLimitLeft)
        {
            transform.position = new Vector3(camTransform.position.x + screenLimitLeft, transform.position.y, 0);
        }

        if(transform.position.y < screenLimitBottom)
        {
            //GameOver
        }
        // else if (BackgroundManager.CanMove() && transform.position.x > screenLimitRight)
        // {
        //     transform.position = new Vector3(screenLimitRight, transform.position.y, 0);
        // }
        // else if (!BackgroundManager.CanMove() && transform.position.x > -screenLimitLeft)
        // {
        //     transform.position = new Vector3(-screenLimitLeft, transform.position.y, 0);
        // }
    }

    void JumpHandler()
    {
        if (isGrounded)
        {
            rb.drag = dragGround;
            moveSpeed = _moveSpeed;
            if (jumpBool)
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
