using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.Linq;
using System;

public class agentController : Agent
{
    public float runSpeed;
    public GameObject bulletObj;
    private float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private float rewardRate = 0.5f;
    private float nextReward = 0.0f;
    [SerializeField] private GameObject[] myBullets;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, -17, 0);

        // Delete all enemies and bullets
        Transform parent = transform.parent;

        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject sibling = parent.GetChild(i).gameObject;
            if (sibling.tag == "Enemy" || sibling.tag == "Bullet" || sibling.tag == "enemyBullet")
            {
                Destroy(sibling);
            }
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut) 
    {
        ActionSegment<int> DiscreteActions = actionsOut.DiscreteActions;

        // -1 = left; 0 = stay; 1 = right
        DiscreteActions[0] = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")) + 1;

        // 0 = nothing; 1 = fire
        if (Input.GetKey(KeyCode.Space))
        {
            DiscreteActions[1] = 1;
        }
        else
        {
            DiscreteActions[1] = 0;
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        // 0 = left; 1 = stay; 2 = right
        int moveX = actions.DiscreteActions[0];
        // -1 = left; 0 = stay; 1 = right (fixing moveX)
        moveX = moveX - 1;
        // 0 = nothing; 1 = fire
        int fire = actions.DiscreteActions[1];
        
        // Do the moving
        transform.localPosition += Vector3.right * moveX * Time.deltaTime * runSpeed;

        if (fire == 1 && Time.time > nextFire)
        {
            AddReward(-0.5f);
            nextFire = Time.time + fireRate;
            GameObject bulletInstance = Instantiate(bulletObj, transform.position, transform.rotation, transform.parent) as GameObject;
            myBullets = myBullets.Append(bulletInstance).ToArray();
        }
        myBullets = myBullets.Where(item => item != null).ToArray();

    }
    private void FixedUpdate()
    {
        if (Time.time > nextReward)
        {
            AddReward(0.25f);
            nextReward = Time.time + rewardRate;
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            SetReward(-10f);
            EndEpisode();
        }
    }
    public void EnemyWon()
    {
        AddReward(-20f);
        EndEpisode();
    }
    public void EnemyKilled()
    {
        AddReward(2f);
    }

}
