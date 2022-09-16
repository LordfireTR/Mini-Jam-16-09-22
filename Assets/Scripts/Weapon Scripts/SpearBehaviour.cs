using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : MonoBehaviour
{
    Collider2D spearCollider;

    Vector3 spearOffset;
    void Start()
    {
        spearCollider = GetComponent<Collider2D>();
        spearCollider.enabled = false;
        spearOffset =   new Vector3(1, 0.1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        SpearHandler();
    }

    public void SpearHandler()
    {
        if(Input.GetMouseButtonDown(0) && !spearCollider.enabled)
        {
            spearCollider.enabled = true;
            transform.localPosition += spearOffset;
        }
        if(Input.GetMouseButtonUp(0) && spearCollider.enabled)
        {
            transform.localPosition -= spearOffset;
            spearCollider.enabled = false;
        }
    }
}
