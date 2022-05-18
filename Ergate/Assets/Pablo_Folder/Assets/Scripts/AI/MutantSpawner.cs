using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpawner : BaseSpawner
{
    private Mutant m_MutantInstance;
    private Mutant m_MutantCopy;
    // Start is called before the first frame update
    private void Start()
    {
        m_EnemiesToBeSpawnedHold = m_EnemiesToBeSpawned;
    }
    private void OnEnable()
    {
        m_MutantInstance = m_Prefab.GetComponent<Mutant>();
        m_MutantCopy = m_MutantInstance.Clone() as Mutant;
        GameObject instantiatedEnemy = Instantiate(m_MutantCopy.gameObject, transform.position, transform.rotation);
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
            GameObject instantiatedEnemy = Instantiate(m_MutantCopy.gameObject, transform.position, transform.rotation);
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
