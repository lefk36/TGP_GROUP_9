using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpawner : BaseSpawner
{
    private Mutant m_MutantInstance;
    private Mutant m_MutantCopy;
    // Start is called before the first frame update
    private void OnEnable()
    {
        m_MutantInstance = m_Prefab.GetComponent<Mutant>();
        m_MutantCopy = m_MutantInstance.Clone() as Mutant;
        //GameObject instantiatedEnemy = Instantiate(m_MutantCopy.gameObject, transform.position, transform.rotation);
        //m_EnemyInstance = instantiatedEnemy;
        //m_EnemiesToBeDeleted.Add(m_EnemyInstance);
        //m_EnemiesToBeSpawned--;
        EnemiesSpawning();
    }

    private void Update()
    {
        //foreach (GameObject enemy in m_EnemiesToBeDeleted)
        //{
        //    if (enemy == null)
        //    {
        //        m_EnemiesToBeDeleted.Remove(enemy);
        //    }
        //}
        if (m_EnemiesToBeDeleted.Count >= m_EnemiesMaxSpawn)
        {
            m_CanSpawn = false;
        }
        else
        {
            m_CanSpawn = true;
        }

        RemoveNullEnemies();
        if (killEnemies)
        {
            killEnemies = false;
            KillAll();
        }
    }

    private void EnemiesSpawning()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (m_EnemiesToBeSpawned > 0)
        {
            if(m_CanSpawn)
            {
                m_EnemiesToBeSpawned--;
                GameObject instantiatedEnemy = Instantiate(m_MutantCopy.gameObject, transform.position, transform.rotation);
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
        foreach (GameObject enemyToDelete in m_EnemiesToBeDeleted)
        {
            Destroy(enemyToDelete);
        }
        gameObject.SetActive(false);
    }
    public override void KillAll()
    {
        foreach(GameObject enemy in m_EnemiesToBeDeleted)
        {
            Destroy(enemy);
        }
    }
    public override void RemoveNullEnemies()
    {
        int i;
        for (i = 0; i < m_EnemiesToBeDeleted.Count; i++)
        {
            if (m_EnemiesToBeDeleted[i] == null)
            {
                m_EnemiesToBeDeleted.RemoveAt(i);

            }
        }

    }
}
