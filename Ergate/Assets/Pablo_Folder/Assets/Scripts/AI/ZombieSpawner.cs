using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : BaseSpawner
{
    private Zombie m_ZombieInstance;
    private Zombie m_ZombieCopy;

    private void OnEnable()
    {
        m_EnemiesToBeSpawnedHold = m_EnemiesToBeSpawned;
        m_ZombieInstance = m_Prefab.GetComponent<Zombie>();
        m_ZombieCopy = m_ZombieInstance.Clone() as Zombie;
        //GameObject instantiatedEnemy = Instantiate(m_ZombieCopy.gameObject, transform.position, transform.rotation);
        //m_EnemyInstance = instantiatedEnemy;
        //m_EnemiesToBeDeleted.Add(m_EnemyInstance);
        //m_EnemiesToBeSpawned--;
        m_CanSpawn = true;
        EnemiesSpawning();
    }

    private void Update()
    {
        
        if (m_EnemiesToBeDeleted.Count >= m_EnemiesMaxSpawn)
        {
            m_CanSpawn = false;
        }
        else
        {
            m_CanSpawn = true;
        }
        //foreach (GameObject enemy in m_EnemiesLeft)
        //{
        //    if (enemy == null)
        //    {
        //        m_EnemiesLeft.Remove(enemy);
        //    }
        //}
        RemoveNullEnemies();
        
        if (killEnemies)
        {
            killEnemies = false;
            KillAll();
        }
    }

    private void EnemiesSpawning()
    {
        if(m_CanSpawn)
        {
            StartCoroutine(EnemySpawn());
        }
        
    }

    IEnumerator EnemySpawn()
    {
        while(m_EnemiesToBeSpawned > 0)
        {
            if(m_CanSpawn)
            {
                m_EnemiesToBeSpawned--;
                GameObject instantiatedEnemy = Instantiate(m_ZombieCopy.gameObject, transform.position, transform.rotation);
                m_EnemyInstance = instantiatedEnemy;
                m_EnemiesToBeDeleted.Add(m_EnemyInstance);
                yield return new WaitForSeconds(m_SpawnRate);
            }
            else
            {
                yield return null;
            }
            
            
        }

    }

    public override void ResetSpawner()
    {
        m_EnemiesToBeSpawned = m_EnemiesToBeSpawnedHold;
        foreach(GameObject enemyToDelete in m_EnemiesToBeDeleted)
        {
            Destroy(enemyToDelete);
        }
        gameObject.SetActive(false);
    }
    public override void KillAll()
    {
        foreach (GameObject enemy in m_EnemiesToBeDeleted)
        {
            Destroy(enemy);
        }
    }
    public override void RemoveNullEnemies()
    {
        int i;
        for(i = 0; i < m_EnemiesToBeDeleted.Count; i++)
        {
            if(m_EnemiesToBeDeleted[i] == null)
            {
                m_EnemiesToBeDeleted.RemoveAt(i);
                
            }
        }

    }

}
