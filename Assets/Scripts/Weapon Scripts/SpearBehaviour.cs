using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : MonoBehaviour
{
    Collider2D spearCollider;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float hitImpact;

    Vector3 spearOffset;
    float baseDamage = 10, attackTime, _attackTime = 0.15f, inputTime, _inputTime;
    bool shouldAttack = false;

    void Start()
    {
        spearCollider = GetComponent<Collider2D>();
        spearCollider.enabled = false;
        spearOffset =   new Vector3(1, 0.1f, 0);
        attackTime = _attackTime;
        _inputTime = 0.1f;
    }

    void Update()
    {
        InputHandler();
    }

    void FixedUpdate()
    {
        SpearHandler();
    }

    void InputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shouldAttack = true;
            inputTime = _inputTime;
        }

        if (inputTime <= 0)
        {
            shouldAttack = false;
        }
        else
        {
            inputTime -= Time.deltaTime;
        }
    }

    public void SpearHandler()
    {
        if(shouldAttack && attackTime == _attackTime)
        {
            spearCollider.enabled = true;
            transform.localPosition += spearOffset;
            attackTime -= Time.deltaTime;
        }
        else if(attackTime < _attackTime && attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }
        else if(attackTime <= 0)
        {
            transform.localPosition -= spearOffset;
            spearCollider.enabled = false;
            attackTime = _attackTime;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            float relativeVelocity = (other.gameObject.GetComponent<Rigidbody2D>().velocity.x - playerRb.velocity.x) * Mathf.Sign(transform.position.x - other.transform.position.x);
            Debug.Log(relativeVelocity);
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(baseDamage * Mathf.Abs(1 + relativeVelocity / 4.0f));
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(hitImpact * ((Vector3.right * (other.transform.position.x - transform.position.x)).normalized + 0.2f * Vector3.up), ForceMode2D.Impulse);
        }
    }
}
