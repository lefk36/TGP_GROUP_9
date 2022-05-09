using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : BaseEnemy
{
    
    private void Start()
    {
        m_Health = 100f;
        m_Damage = 20f;
        m_ZombieAgent = GetComponent<NavMeshAgent>();
    }


}
