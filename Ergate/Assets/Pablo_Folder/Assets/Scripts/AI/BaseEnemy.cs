using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour, IEnemy
{
    public float m_Health;
    public int m_HealthDamage;
    public int m_PoiseDamage;
    public float m_stoppingDistance;
    public float m_RotationRate;
    [HideInInspector] public GameObject m_Target;
    [HideInInspector] public NavMeshAgent m_Agent;
    [HideInInspector] public PlayerPoiseAndHealth m_PlayerStats;
    [HideInInspector] public Animator m_Animator;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool m_CanAttack;
    public float m_AttackRate;
    [HideInInspector] public bool m_IsAttacking;
    [HideInInspector] public EnemiesCameraLock m_CameraLock;


    public void TakeDamage(float damageTaken)
    {
        m_Animator.SetTrigger("TakeDamage");
        m_Health -= damageTaken;
        if (m_Health <= 0f)
        {
            m_Animator.SetTrigger("IsDead");
            m_CameraLock.m_LockOn = false;
            m_CameraLock.m_TargetableEnemies.Remove(gameObject);
            StartCoroutine(EnemyDeath());
        }
    }

    public void DealDamage(Vector3 attackDirection, int healthDamageDealt, int poiseDamageDealt)
    {
        m_PlayerStats.TakeDamage(attackDirection, healthDamageDealt, poiseDamageDealt);
    }

    public void FacePlayer()
    {
        Vector3 faceDirection = (m_Target.transform.position - transform.position).normalized;
        Quaternion directionToRotate = Quaternion.LookRotation(new Vector3(faceDirection.x, 0, faceDirection.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, directionToRotate, Time.deltaTime * m_RotationRate);

    }

    public void SetEnemyPath()
    {
        if(!m_IsAttacking)
        {
            Vector3 offset = transform.rotation * -Vector3.forward * m_stoppingDistance;
            Vector3 targetPos = new Vector3(m_Target.transform.position.x, transform.position.y, m_Target.transform.position.z) + offset;
            if (m_Agent.enabled)
            {
                m_Agent.SetDestination(targetPos);
            }
        }
    }
    public void ReEnableAgent()
    {
        if(m_Agent.enabled == false)
        {
            m_Agent.enabled = true;
        }
    }

    public abstract BaseEnemy Clone();

    IEnumerator EnemyDeath()
    {
        if (m_Agent.enabled)
        {
            m_Agent.SetDestination(transform.position);
            
        }
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

}


interface IEnemy
{
    void TakeDamage(float damageTaken);
    void DealDamage(Vector3 attackDirection, int healthDamageDealt, int poiseDamageDealt);
    void FacePlayer();
    void SetEnemyPath();
}
