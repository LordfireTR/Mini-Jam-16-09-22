using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    Collider2D swordCollider;
    float swordSwingRad, swingRate;
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
        if (Input.GetMouseButton(0))
        {
            swingRate += 10 * Time.deltaTime;
            swordCollider.enabled = true;
            transform.rotation = Quaternion.Slerp(swordIdle, swordActive, t);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            transform.rotation = swordIdle;
            swordCollider.enabled = false;
            swingRate = 0;
        }
    }
}
