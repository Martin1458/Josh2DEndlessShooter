using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private float spawnRate = 2f;
    private float nextSpawn = 0.0f;
    private Vector3 newPosition;
    private float randomOffset;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            float randomOffset = UnityEngine.Random.Range(-17, 17);
            Vector3 newPosition = new Vector3(transform.position.x + randomOffset, transform.position.y, transform.position.z);
            GameObject enemyInstance = Instantiate(Enemy, newPosition, transform.rotation, transform.parent) as GameObject;
        }
    }
}
