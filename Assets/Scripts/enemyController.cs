using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private GameObject agentObj;
    private agentController agentScript;

    public GameObject enemyBulletObj;
    private float fireRate = 2f;
    private float nextFire = 0.0f;

    // 1 = right; -1 = left
    private int side;
    // random height that the enemy hovers at
    private int randomHeight;

    private bool goingDown = false;
    private void Start()
    {
        side = UnityEngine.Random.Range(0, 2) * 2 - 1;
        randomHeight = UnityEngine.Random.Range(0, 17);

        //transform.localPosition = Vector3.zero;
        //float randomOffset = UnityEngine.Random.Range(-17, 17);
        //Vector3 newPosition = new Vector3(transform.localPosition.x + randomOffset, transform.localPosition.y, transform.localPosition.z);
        //transform.localPosition = newPosition;

        // Find the Agent
        Transform parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject sibling = parent.GetChild(i).gameObject;
            if (sibling.tag == "Player")
            {
                agentObj = sibling;
                agentScript = agentObj.GetComponent<agentController>();
            }
        }
    }
    void FixedUpdate()
    {
        
        if (transform.localPosition.y > randomHeight || side == 0)
        {
            transform.localPosition += new Vector3(0, -7, 0) * Time.deltaTime;
        }
        else
        {
            transform.localPosition += new Vector3(7 * side, 0, 0) * Time.deltaTime;
        }

        if (Time.time > nextFire && !goingDown)
        {
            nextFire = Time.time + fireRate;
            Instantiate(enemyBulletObj, transform.position, transform.rotation, transform.parent);
        }

        if (-10 < transform.localPosition.x && transform.localPosition.x < 10)
        {
            if(UnityEngine.Random.Range(0, 1000) == 0) 
            {
                goingDown = true;
                side = 0; 
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            if (collision.name == "Bottom")
            {
                if (agentObj == null) { Debug.Log("Error: Agent not found."); }
                agentScript.EnemyWon();
                Destroy(this.gameObject);
            } else if (collision.name != "Top")
            {
                if(transform.localPosition.x > 0)
                {
                    side = -1;
                } else
                {
                    side = 1;
                }
            }

        }
    }
}
