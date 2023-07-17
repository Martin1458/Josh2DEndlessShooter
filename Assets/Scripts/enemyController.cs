using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject agentObj;
    private agentController agentScript;

    private void Start()
    {
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
        transform.localPosition += new Vector3(0, -7, 0) * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            if (agentObj == null) { Debug.Log("Error: Agent not found."); }
            agentScript.EnemyWon();
            Destroy(this.gameObject);
        }
    }
}
