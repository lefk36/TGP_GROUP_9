using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : BaseEnemy
{
    private void Awake()
    {
        //EventManager.current.onEnemyDestroy += OnZombieKilled;
    }

    private void Start()
    {
        m_Health = 150f;
        m_HealthDamage = 20;
        m_PoiseDamage = 20;
        m_PlayerStats = GameObject.FindObjectOfType<PlayerPoiseAndHealth>();
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Target = GameObject.FindGameObjectWithTag("Player");
        m_CameraLock = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<EnemiesCameraLock>();
        rb = GetComponent<Rigidbody>();
        gravityScaleScript = GetComponent<GravityScaler>();
        m_CanAttack = true;
        m_IsAttacking = false;
        
    }

    private void Update()
    {
        Debug.Log(rb.velocity);
        Debug.Log(m_IsTakingDamage);
        if(!m_IsTakingDamage && (rb.velocity.y > -0.1f && rb.velocity.y < 0.1f))
        {
            m_Agent.enabled = true;
        }

        FacePlayer();

        Vector3 enemyToPlayer = m_Target.transform.position - transform.position;
        if (enemyToPlayer.magnitude < m_stoppingDistance + m_Agent.radius)
        {
            m_Animator.SetBool("IsRunning", false);
        }
        else
        {
            if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieAttack") && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieTakeDamage"))
            {
                SetEnemyPath();
            }
            m_Animator.SetBool("IsRunning", true);
        }

        if(rb.velocity.y < 0f)
        {
            m_Animator.SetTrigger("Falling");
        }
        else
        {
            m_Animator.SetTrigger("BackToIdle");
        }
    }
    private void FixedUpdate()
    {
        if (lockFalling)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
            gravityScaleScript.gravityScale = 0;
        }
        else
        {
            gravityScaleScript.gravityScale = 1;
        }
        if (rb.velocity.y > 0.2f && !lockFalling) //when rising
        {
            gravityScaleScript.gravityScale = 3.0f;
        }
        if (rb.velocity.y < 0 && !lockFalling) //when falling
        {

            gravityScaleScript.gravityScale = 6.0f;
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Should fire attack animation");
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
