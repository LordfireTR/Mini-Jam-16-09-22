using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth, moveSpeed, armor, armorMultiplier, moveDirection;
    float screenLimitBottom = -11;
    [SerializeField] HealthBarBehaviour HealthBarBehaviour;
    Transform playerTransform;
    Rigidbody2D enemyRb;
    AudioSource AudioSource;
    bool facingRight;
    // Start is called before the first frame update
    void Awake()
    {
        if (maxHealth == 0)
        {
            maxHealth = 20;
        }
        armorMultiplier = 0.75f;
        enemyRb = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();
        playerTransform = transform.parent.GetComponent<SpawnManager>().playerTransform;
    }

    void Start()
    {
        facingRight = true;
        currentHealth = maxHealth;
        AudioSource.pitch = 3f;
        AudioSource.volume = 0.3f;
    }

    void Update()
    {
        HealthBarHandler();
    }

    void FixedUpdate()
    {
        HandleDeath();
        HandleMovement();
    }

    public void TakeDamage(float dmg)
    {
        float dmgTaken = Mathf.Round(dmg * Mathf.Pow(armorMultiplier, armor));
        currentHealth -= dmgTaken;
        Debug.Log(dmgTaken);
        AudioSource.Play();
    }

    public void HandleDeath()
    {
        if(currentHealth <= 0 || transform.position.y < screenLimitBottom)
        {
            transform.eulerAngles = 90 * Vector3.right;
            gameObject.SetActive(false);
            Debug.Log("Death!");
        }
    }

    private void HandleMovement()
    {
        moveDirection = Mathf.Sign(playerTransform.position.x - transform.position.x);
        enemyRb.AddForce(moveSpeed * moveDirection * Vector3.right);

        if (moveDirection == -1 && facingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (moveDirection == 1 && !facingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
    }

    void HealthBarHandler()
    {
        HealthBarBehaviour.maxHealth = maxHealth;
        HealthBarBehaviour.currentHealth = currentHealth;
    }
}
