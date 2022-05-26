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
    public bool lockFalling;

    [HideInInspector] public GravityScaler gravityScaleScript;
    [HideInInspector] public GameObject m_Target;
    [HideInInspector] public NavMeshAgent m_Agent;
    [HideInInspector] public PlayerPoiseAndHealth m_PlayerStats;
    [HideInInspector] public Animator m_Animator;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool m_CanAttack;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool m_IsTakingDamage;
    [HideInInspector] public float m_AttackRate;
    [HideInInspector] public bool m_IsAttacking;
    [HideInInspector] public EnemiesCameraLock m_CameraLock;
    [HideInInspector] public SphereCollider m_GroundCollider;
    public LayerMask m_Ground;
    public bool isOnGround;

    public GameObject itemPrefab;

    [HideInInspector] public int itemNum;
    [HideInInspector] public int randNum;

    protected gameManager manager;

    public void TakeDamage(float damageTaken, bool p_lockFalling)
    {

        lockFalling = p_lockFalling;
        m_Agent.enabled = false;
        m_IsTakingDamage = true;
        m_Animator.SetTrigger("TakeDamage");
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieTakeDamage"))
        {
            m_Animator.Play("ZombieTakeDamage", -1, 0f);
        }
        m_Health -= damageTaken;
        if (m_Health <= 0f && !isDead)
        {
            m_Animator.SetTrigger("IsDead");
            lockFalling = false;
            isDead = true;
            m_CameraLock.m_LockOn = false;
            if (m_CameraLock.m_TargetableEnemies.Contains(gameObject))
            {
                m_CameraLock.m_TargetableEnemies.Remove(gameObject);
            }
            StartCoroutine(EnemyDeath());
        }
    }

    public void DealDamage(Vector3 attackDirection, int healthDamageDealt, int poiseDamageDealt)
    {
        m_PlayerStats.TakeDamage(attackDirection, healthDamageDealt, poiseDamageDealt);
    }

    public void FacePlayer()
    {
        if(m_Agent.enabled)
        {
            Vector3 faceDirection = (m_Target.transform.position - transform.position).normalized;
            Quaternion directionToRotate = Quaternion.LookRotation(new Vector3(faceDirection.x, 0, faceDirection.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, directionToRotate, Time.deltaTime * m_RotationRate);
        }
        

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
    public void NotTakingDamage()
    {
        m_IsTakingDamage = false;
        lockFalling = false;
    }

    public abstract BaseEnemy Clone();

    IEnumerator EnemyDeath()
    {
        m_Agent.enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        DropItem();
    }

    public void DropItem()
    {
        randNum = Random.Range(0, 101);
        if (randNum > 95)
        {
            itemNum = 1;
            GameObject pickableItem = Instantiate(itemPrefab, transform.position, Quaternion.identity) as GameObject;
        }
        else if(randNum >= 45 && randNum <= 75)
        {
            itemNum = 1;
            GameObject pickableItem = Instantiate(itemPrefab, transform.position, Quaternion.identity) as GameObject;
        }

    }
}


interface IEnemy
{
    void TakeDamage(float damageTaken, bool p_lockFalling);
    void DealDamage(Vector3 attackDirection, int healthDamageDealt, int poiseDamageDealt);
    void FacePlayer();
    void SetEnemyPath();
    void NotTakingDamage();

    void DropItem();
}
