using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomCheck : MonoBehaviour
{
    public List<BaseSpawner> m_SpawnerList;
    private PlayerPoiseAndHealth m_PlayerStats;
    [SerializeField] private GameObject m_WallWeb;

    private void Start()
    {

        m_PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPoiseAndHealth>();
        
    }
    private void Update()
    {
        if(m_PlayerStats.m_IsDead)
        {
            StartCoroutine(SpawnerResetDelay());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach(BaseSpawner spawner in m_SpawnerList)
            {
                spawner.gameObject.SetActive(true);
            }

            m_WallWeb.SetActive(true);
        }
    }

    IEnumerator SpawnerResetDelay()
    {
        yield return new WaitForSeconds(2.2f);
        foreach(BaseSpawner spawner in m_SpawnerList)
        {
            spawner.ResetSpawner();
        }

        m_WallWeb.SetActive(false);

    }
}
