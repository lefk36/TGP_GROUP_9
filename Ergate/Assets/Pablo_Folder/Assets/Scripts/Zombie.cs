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
        m_IsAttacking = false;
    }

   
    private void Update()
    {
        FacePlayer();
        Vector3 enemyToPlayer = m_Target.transform.position - transform.position;
        if (enemyToPlayer.magnitude < m_Agent.stoppingDistance)
        {
            m_Animator.SetBool("IsRunning", false);
        }
        else
        {
            if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieAttack"))
            {
                SetEnemyPath();
            }
            m_Animator.SetBool("IsRunning", true);
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Should fire attack animation");
            if(!m_Animator.GetBool("IsRunning"))
            {
                m_Animator.SetTrigger("IsAttacking");
            }

            if (m_IsAttacking && m_CanAttack)
            {
                DealDamage(Vector3.zero, m_HealthDamage, m_PoiseDamage);
                StartCoroutine(AttackReset());
                
            }
        }
    }

    public override BaseEnemy Clone()
    {
        return this.MemberwiseClone() as BaseEnemy;
    }

    public void ShouldDealDamage(string check)
    {
        if(check.Equals("DealDamage"))
        {
            m_IsAttacking = true;
            //StartCoroutine(AttackReset());
        }
    }

    public void StopAttacking(string check)
    {
        if(check.Equals("NoDealingDamage"))
        {
            m_IsAttacking = false;
        }
    }

    IEnumerator AttackReset()
    {
        m_CanAttack = false;
        yield return new WaitForSeconds(m_AttackRate);
        m_CanAttack = true;


    }
}
