using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MeleeEnemyController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    [SerializeField] public Transform target;
    public float attackRange = 2f;
    public float movementSpeed = 3.5f;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        agent.stoppingDistance = attackRange;
        agent.speed = movementSpeed;
    }
    private void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        MoveToTarget();
        if (dist <= agent.stoppingDistance)
        {
            FaceTarget();
        }
    }
    public void Attack()
    {
        //I tried to add in attacking here but couldn't get it to work properly so I put it in another script on another object for the demo
    }
    void FaceTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;                                    //If the player gets too close without 
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));                    //this script on the enemy object doesn't turn
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 5f);       //
    }
    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
    }
}

