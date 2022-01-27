using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    //Behaviour for a shooting enemy
    #region shooting stuff 
    public GameObject m_bullet;
    public GameObject m_firingPoint;
    bool m_hasAttacked;
    float m_timeBetweenAttacks = 3f;
    #endregion
    #region targetting
    public NavMeshAgent m_agent;
    public Transform m_player;
    public LayerMask m_whatIsGround, m_whatIsPlayer;
    public float m_sightRange, m_attackRange;
    public bool m_playerInSightRange, m_playerInAttackRange;
    #endregion
    void Awake()
    {
        m_player = GameObject.Find("Player").transform;
        m_agent = GetComponent<NavMeshAgent>();

    }
    private void Update()
    {
        m_playerInSightRange = Physics.CheckSphere(transform.position, m_sightRange, m_whatIsPlayer);
        m_playerInAttackRange = Physics.CheckSphere(transform.position, m_attackRange, m_whatIsPlayer);

        if (!m_playerInSightRange && !m_playerInAttackRange)
            Patrolling();

        if (m_playerInSightRange && !m_playerInAttackRange)
            FollowPlayer();

        if (m_playerInSightRange && m_playerInAttackRange)
            AttackPlayer();

    }
    void Patrolling()
    {
        Debug.Log($"patrolling");
    }
    void FollowPlayer()
    {
        m_agent.SetDestination(m_player.position);
    }
    void AttackPlayer()
    {
        m_agent.SetDestination(transform.position);
        transform.LookAt(m_player);

        if (!m_hasAttacked)
        {
            Debug.Log("bang bang");
            //Rigidbody rb = Instantiate(m_bullet, m_firingPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 50f, ForceMode.Impulse);
            Shoot();
            m_hasAttacked = true;
            Invoke(nameof(ResetAttack), m_timeBetweenAttacks);
        }
    }
    void ResetAttack()
    {
        m_hasAttacked = false;
    }
    public void Shoot()
    {
        Rigidbody rb = Instantiate(m_bullet, m_firingPoint.transform.position, Quaternion.identity).GetComponentInParent<Rigidbody>();
        rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        rb.AddForce(transform.up * 2f, ForceMode.Impulse);
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
    }
}
