using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Enemies = new List<GameObject>();
    public Transform playerTransform;
    float spawnPosY;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosY = playerTransform.position.y;
        SpawnEnemy(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(int index)
    {
        Instantiate(Enemies[index], SpawnPosition(), Enemies[index].transform.rotation, transform);
    }

    Vector3 SpawnPosition()
    {
        return new Vector3(17, spawnPosY, 0);
    }
}
