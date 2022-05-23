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
        m_Health = 200f;
        m_PlayerStats = GameObject.FindObjectOfType<PlayerPoiseAndHealth>();
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        m_CanAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        ReEnableAgent();
        FacePlayer();
        Vector3 playerFloorPos = new Vector3(m_Target.transform.position.x, transform.position.y, m_Target.transform.position.z);
        Vector3 enemyToPlayer = playerFloorPos - transform.position;
        if (enemyToPlayer.magnitude <= m_Agent.stoppingDistance)
        {
            m_Animator.SetBool("IsRunning", false);
            if(m_CanAttack)
            {
                m_Animator.SetBool("InRange", true);
               
                if(m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !m_Animator.IsInTransition(0))
                {
                    StartCoroutine(ResetShoot());
                    
                }
            }
            else
            {
                Debug.Log("Reloding");
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
            SetEnemyPath();
            m_Animator.SetBool("IsRunning", true);
            m_Animator.SetBool("InRange", false);

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
        Debug.Log("CanAttackAgain");
        m_CanAttack = true;
    }
}
