using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour, IEnemy
{
    public float m_Health;
    public float m_Damage;
    public GameObject m_Target;
    [HideInInspector] public NavMeshAgent m_ZombieAgent;

    public void TakeDamage(float damageTaken)
    {
        m_Health -= damageTaken;
        if (m_Health <= 0f)
        {
            //death event
        }
    }

}


interface IEnemy
{
    void TakeDamage(float damageTaken);
    //void DealDamage(float damageDealt);


}
