using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D playerRb;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
    }
    void LateUpdate()
    {
        if (transform.position.x < player.transform.position.x - 5)
        {
            transform.position += Vector3.right * playerRb.velocity.x * Time.deltaTime;
        }
    }
}
