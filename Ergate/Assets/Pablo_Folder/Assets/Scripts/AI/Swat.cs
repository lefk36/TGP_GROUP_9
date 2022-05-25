using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swat : BaseEnemy
{
    [SerializeField] private GameObject m_BulletPref;
    [SerializeField] private GameObject m_GunMuzzle;
    // Start is called before the first frame update
    void Start()
    {
        m_Health = 100f;
        m_PlayerStats = GameObject.FindObjectOfType<PlayerPoiseAndHealth>();
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        gravityScaleScript = GetComponent<GravityScaler>();
        m_CameraLock = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<EnemiesCameraLock>();
        m_CanAttack = true;
        m_GroundCollider = transform.GetChild(0).GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        isOnGround = Physics.SphereCast(m_GroundCollider.bounds.center, m_GroundCollider.radius - 0.1f, Vector3.down, out hit, m_GroundCollider.bounds.extents.y+0.1f, m_Ground);

        if (!m_IsTakingDamage && isOnGround)
        {
            lockFalling = false;
            m_Agent.enabled = true;
        }

        FacePlayer();
        Vector3 playerFloorPos = new Vector3(m_Target.transform.position.x, transform.position.y, m_Target.transform.position.z);
        Vector3 enemyToPlayer = playerFloorPos - transform.position;
        if (enemyToPlayer.magnitude <= m_Agent.stoppingDistance)
        {
            m_Animator.SetBool("IsRunning", false);
            if(m_CanAttack && m_Agent.enabled)
            {
                m_Animator.SetBool("InRange", true);
               
                if(m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !m_Animator.IsInTransition(0))
                {
                    StartCoroutine(ResetShoot());
                    
                }
            }
            else
            {
                m_Animator.SetBool("InRange", false);
                
            }
            
            //else
            //{
            //    Debug.Log("CannotAttack");
            //    m_Animator.SetBool("InRange", false);
            //}
            
        }
        else
        {
            if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("SwatShooting") && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("SwatTakeDamage") && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("SwatFalling"))
            {
                SetEnemyPath();
            }
            m_Animator.SetBool("IsRunning", true);
            m_Animator.SetBool("InRange", false);

        }

        if (rb.velocity.y < -0.1f)
        {
            m_Animator.SetBool("IsFalling", true);
        }
        else if (rb.velocity.y > -0.1f && rb.velocity.y < 0.1f)
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

    public void Shoot()
    {
        Instantiate(m_BulletPref, m_GunMuzzle.transform.position, m_GunMuzzle.transform.rotation);
    }


    public override BaseEnemy Clone()
    {
        return this.MemberwiseClone() as BaseEnemy;
    }

    IEnumerator ResetShoot()
    {
        m_CanAttack = false;
        yield return new WaitForSeconds(4f);
        m_CanAttack = true;
    }
}
