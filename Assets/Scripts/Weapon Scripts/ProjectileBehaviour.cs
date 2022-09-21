using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] float baseDamage, hitImpact;
    [SerializeField] Transform mainCam;

    void Update()
    {
        if (transform.position.x > 18 + mainCam.position.x)
        {
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(baseDamage);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(hitImpact * ((Vector3.right * (other.transform.position.x - transform.position.x)).normalized + 0.2f * Vector3.up), ForceMode2D.Impulse);
            gameObject.SetActive(false);
        }
    }
}
