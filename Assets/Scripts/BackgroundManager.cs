using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] GameObject player, SpawnManager;
    Rigidbody2D playerRb;
    PlayerBehaviour PlayerBehaviour;

    Vector3 backgroundSpeed;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        PlayerBehaviour = player.GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        backgroundSpeed = playerRb.velocity;

        if (player.transform.position.x > 0 && backgroundSpeed.x > 0 && CanMove())
        {
            transform.position -= Vector3.right * backgroundSpeed.x * Time.deltaTime * player.transform.position.x/PlayerBehaviour.screenLimitRight;
        }

        if (transform.position.x <= -40)
        {
            transform.position = Vector3.zero;
        }
    }

    public bool CanMove()
    {
        if (SpawnManager.transform.childCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
