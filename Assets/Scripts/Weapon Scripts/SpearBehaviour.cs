using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : MonoBehaviour
{
    Collider2D spearCollider;
    [SerializeField] Rigidbody2D playerRb;

    Vector3 spearOffset;
    float baseDamage = 10, attackTime, _attackTime = 0.15f;

    void Start()
    {
        spearCollider = GetComponent<Collider2D>();
        spearCollider.enabled = false;
        spearOffset =   new Vector3(1, 0.1f, 0);
        attackTime = _attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        SpearHandler();
    }

    public void SpearHandler()
    {
        if(Input.GetMouseButtonDown(0) && attackTime == _attackTime)
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
            float relativeVelocity = other.gameObject.GetComponent<Rigidbody2D>().velocity.x + playerRb.velocity.x;
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(baseDamage * Mathf.Abs(1 + relativeVelocity / 4.0f));
        }
    }
}
