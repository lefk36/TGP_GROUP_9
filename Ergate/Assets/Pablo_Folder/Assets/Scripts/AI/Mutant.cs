using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mutant : BaseEnemy
{
    private void Awake()
    {
        //EventManager.current.onEnemyDestroy += OnZombieKilled;
    }

    private void Start()
    {
        m_Health = 300f;
        m_HealthDamage = 40;
        m_RotationRate = 5;
        m_PoiseDamage = 30;
        m_AttackRate = 1f;
        m_PlayerStats = GameObject.FindObjectOfType<PlayerPoiseAndHealth>();
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Target = GameObject.FindGameObjectWithTag("Player");
        m_CameraLock = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<EnemiesCameraLock>();
        rb = GetComponent<Rigidbody>();
        m_CanAttack = true;
        m_IsAttacking = false;
    }

    private void Update()
    {
        FacePlayer();

        Vector3 enemyToPlayer = m_Target.transform.position - transform.position;
        if (enemyToPlayer.magnitude < m_stoppingDistance + m_Agent.radius)
        {
            m_Animator.SetBool("IsRunning", false);
        }
        else
        {
            if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieAttack") && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieTakeDamage"))
            {
                SetEnemyPath();
            }
            m_Animator.SetBool("IsRunning", true);
        }

    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Should fire attack animation");
            if (!m_Animator.GetBool("IsRunning"))
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
        if (check.Equals("DealDamage"))
        {
            m_IsAttacking = true;
            //StartCoroutine(AttackReset());
        }
    }

    public void StopAttacking(string check)
    {
        if (check.Equals("NoDealingDamage"))
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
