using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour, IEnemy
{
    public float m_Health;
    public int m_HealthDamage;
    public int m_PoiseDamage;
    public float m_RotationRate;
    [HideInInspector] public GameObject m_Target;
    [HideInInspector] public NavMeshAgent m_Agent;
    [HideInInspector] public PlayerPoiseAndHealth m_PlayerStats;
    [HideInInspector] public Animator m_Animator;
    [HideInInspector] public bool m_CanAttack;
    public float m_AttackRate;


    public void TakeDamage(float damageTaken)
    {
        m_Health -= damageTaken;
        if (m_Health <= 0f)
        {
            Destroy(gameObject);
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
        m_Agent.SetDestination(m_Target.transform.position);
    }

}


interface IEnemy
{
    void TakeDamage(float damageTaken);
    void DealDamage(Vector3 attackDirection, int healthDamageDealt, int poiseDamageDealt);
    void FacePlayer();
    void SetEnemyPath();


}
