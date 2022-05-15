using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatSpawner : BaseSpawner
{
    private Swat m_SwatInstance;
    private Swat m_SwatCopy;

    private void Start()
    {
        m_EnemiesToBeSpawnedHold = m_EnemiesToBeSpawned;
    }
    private void OnEnable()
    {
        m_SwatInstance = m_Prefab.GetComponent<Swat>();
        m_SwatCopy = m_SwatInstance.Clone() as Swat;
        GameObject instantiatedEnemy = Instantiate(m_SwatCopy.gameObject, transform.position, transform.rotation);
        m_EnemiesToBeDeleted.Add(instantiatedEnemy);
        m_EnemiesToBeSpawned--;
        EnemiesSpawning();
    }

    private void EnemiesSpawning()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (m_EnemiesToBeSpawned > 0)
        {
            yield return new WaitForSeconds(m_SpawnRate);
            m_EnemiesToBeSpawned--;
            GameObject instantiatedEnemy = Instantiate(m_SwatCopy.gameObject, transform.position, transform.rotation);
            m_EnemiesToBeDeleted.Add(instantiatedEnemy);
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
}
