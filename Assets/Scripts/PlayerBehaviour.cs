using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed, _moveSpeed, jumpSpeed, gravityMultiplier, inputHorizontal, inputVertical, dragGround, dragAir;

    Rigidbody2D rb;
    public bool isGrounded, facingRight;

    public List<GameObject> weaponList = new List<GameObject>();
    GameObject spear, sword;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PrepareWeapons();
        _moveSpeed = 5;
        jumpSpeed = 12;
        gravityMultiplier = 2.5f;
        dragGround = 4;
        dragAir = 1;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovementHandler();
        JumpHandler();
        FallHandler();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponSwap(1);
        }
    }

    public void MovementHandler()
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

    public void JumpHandler()
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

    public void FallHandler()
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

    void PrepareWeapons()
    {
        spear = weaponList[0];
    }

    void WeaponSwap(int i)
    {
        foreach (GameObject item in weaponList)
        {
            item.SetActive(false);
        }
        switch (i)
        {
            case 1:
            weaponList[0].SetActive(true);
            break;

            case 2:
            weaponList[1].SetActive(true);
            break;
            
            default:
            weaponList[0].SetActive(true);
            break;
        }
    }
}
