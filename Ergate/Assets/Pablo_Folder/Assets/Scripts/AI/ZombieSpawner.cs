using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : BaseSpawner
{
    private Zombie m_ZombieInstance;
    private Zombie m_ZombieCopy;

    private void OnEnable()
    {
        m_ZombieInstance = m_Prefab.GetComponent<Zombie>();
        m_ZombieCopy = m_ZombieInstance.Clone() as Zombie;
        Instantiate(m_ZombieCopy.gameObject, transform.position, transform.rotation);
        EnemiesSpawning();
    }

    private void EnemiesSpawning()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while(m_EnemiesToBeSpawned > 0)
        {
            yield return new WaitForSeconds(m_SpawnRate);
            m_EnemiesToBeSpawned--;
            Instantiate(m_ZombieCopy.gameObject, transform.position, transform.rotation);
        }
        
    }

}
