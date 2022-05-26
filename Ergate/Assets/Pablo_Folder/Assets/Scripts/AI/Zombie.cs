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
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        manager = GameObject.Find("LevelManager").GetComponent<gameManager>();
        rb = GetComponent<Rigidbody>();
        gravityScaleScript = GetComponent<GravityScaler>();
        m_CanAttack = true;
        m_IsAttacking = false;
        m_GroundCollider = transform.GetChild(0).GetComponent<SphereCollider>();
        StartCoroutine(WaitForSceneLoad());
        
    }

    private void Update()
    {

        RaycastHit hit;
        isOnGround = Physics.SphereCast(m_GroundCollider.bounds.center, m_GroundCollider.radius - 0.1f, Vector3.down, out hit, m_GroundCollider.bounds.extents.y + 0.1f, m_Ground);

        if(!m_IsTakingDamage && isOnGround)
        {
            lockFalling = false;
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
            if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieAttack") && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieTakeDamage") && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieFalling"))
            {
                SetEnemyPath();
            }
            m_Animator.SetBool("IsRunning", true);
        }

        if(Mathf.Abs(rb.velocity.y) > 0.1 && !isOnGround)
        {
            m_Animator.SetBool("IsFalling", true);
        }
        else if(isOnGround)
        {
            m_Animator.SetBool("IsFalling", false);
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
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!m_Animator.GetBool("IsRunning") && m_Agent.enabled)
            {
                m_Animator.SetTrigger("IsAttacking");
            }

            if (m_IsAttacking && m_CanAttack && m_Agent.enabled)
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
    IEnumerator WaitForSceneLoad()
    {
        while (!manager.playerScene.isLoaded)
        {
            yield return null;
        }
        if (m_PlayerStats == null)
        {
            m_PlayerStats = GameObject.FindObjectOfType<PlayerPoiseAndHealth>();
        }
        if (m_Target == null)
        {
            m_Target = GameObject.FindGameObjectWithTag("Player");
        }
        if(m_CameraLock == null)
        {
            m_CameraLock = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemiesCameraLock>();
        }
        yield return null;
    }
}
