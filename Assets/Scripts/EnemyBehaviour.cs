using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float health, moveSpeed, armor, armorMultiplier, moveDirection;
    Transform playerTransform;
    Rigidbody2D enemyRb;
    bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        if (health == 0)
        {
            health = 20;
        }
        armorMultiplier = 0.75f;
        facingRight = true;
        enemyRb = GetComponent<Rigidbody2D>();
        playerTransform = transform.parent.GetComponent<SpawnManager>().playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDeath();
        HandleMovement();
    }

    public void TakeDamage(float dmg)
    {
        health -= Mathf.Round(dmg * Mathf.Pow(armorMultiplier, armor));
    }

    public void HandleDeath()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void HandleMovement()
    {
        moveDirection = Mathf.Sign(transform.position.x - playerTransform.position.x);
        enemyRb.velocity = Vector3.left * moveSpeed * moveDirection + Vector3.up * enemyRb.velocity.y;

        if (moveDirection == 1 && facingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (moveDirection == -1 && !facingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
    }
}
