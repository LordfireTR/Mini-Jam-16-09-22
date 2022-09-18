using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBehaviour : MonoBehaviour
{
    [SerializeField] GameObject fireBall;
    [SerializeField] float fireBallSpeed;
    Vector3 firePoint;

    // Start is called before the first frame update
    void Start()
    {
        fireBall.SetActive(false);
        firePoint = fireBall.transform.localPosition;
    }

    void Update()
    {
        MagicControl();
    }
    
    void FixedUpdate()
    {
        MagicHandler();
    }

    void MagicControl()
    {
        if(Input.GetMouseButtonDown(0) && !fireBall.activeInHierarchy)
        {
            fireBall.SetActive(true);
        }
    }

    void MagicHandler()
    {
        if (fireBall.activeInHierarchy)
        {
            fireBall.transform.localPosition += fireBallSpeed * Vector3.right * Time.deltaTime;
        }
        else
        {
            fireBall.transform.localPosition = firePoint;
        }

    }
}
