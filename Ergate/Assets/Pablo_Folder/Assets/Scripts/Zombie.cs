using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : BaseEnemy
{
    
    private void Start()
    {
        m_Health = 100f;
        m_HealthDamage = 20;
        m_PoiseDamage = 20;
        m_PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPoiseAndHealth>();
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Target = GameObject.FindGameObjectWithTag("Player");
        m_CanAttack = true;
    }

    private void Update()
    {
        FacePlayer();
        SetEnemyPath();

        Vector3 enemyToPlayer = m_Target.transform.position - transform.position;
        if (enemyToPlayer.magnitude < m_Agent.stoppingDistance && m_CanAttack)
        {
            if (m_Target.CompareTag("Player"))
            {
                DealDamage(Vector3.zero, m_HealthDamage, m_PoiseDamage);
                m_CanAttack = false;
                StartCoroutine(AttackReset());
            }
            else
            {
                return;
            }
        }
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSeconds(m_AttackRate);
        m_CanAttack = true;
    }
}
