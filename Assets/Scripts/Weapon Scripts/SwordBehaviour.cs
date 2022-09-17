using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    Collider2D swordCollider;
    float swordSwingRad, swingRate, baseDamage = 25;
    Quaternion swordIdle, swordActive;
    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = true;
        swordSwingRad = 90;
        swordIdle = transform.localRotation;
        swordActive = swordIdle;
        swordActive.eulerAngles -= Vector3.forward * swordSwingRad;
    }

    // Update is called once per frame
    void Update()
    {
        SwordHandler(swingRate);
    }

    public void SwordHandler(float t)
    {
        if (Input.GetMouseButtonDown(0) && swingRate == 0)
        {
            swingRate += 10 * Time.deltaTime;
            swordCollider.enabled = true;
            transform.localRotation = Quaternion.Slerp(swordIdle, swordActive, t);
        }
        else if (swingRate > 0 && swingRate < 1)
        {
            swingRate += 10 * Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(swordIdle, swordActive, t);
        }
        else if (swingRate >= 1)
        {
            transform.localRotation = swordIdle;
            swordCollider.enabled = false;
            swingRate = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(baseDamage);
            Debug.Log("hit!");
        }
    }
}
