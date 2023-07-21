using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private float spawnRate = 5f;
    private float nextSpawn = 0.0f;
    private Vector3 newPosition;
    private float randomOffset;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextSpawn)
        {
            //decreesing the spawnRate, so that enemies spawn more often over time
            if (spawnRate > 0.6f) { spawnRate = spawnRate - 0.01f; }
            nextSpawn = Time.time + spawnRate;
            float randomOffset = UnityEngine.Random.Range(-17, 17);
            Vector3 newPosition = new Vector3(transform.position.x + randomOffset, transform.position.y, transform.position.z);
            Instantiate(Enemy, newPosition, transform.rotation, transform.parent);
        }
    }
}
