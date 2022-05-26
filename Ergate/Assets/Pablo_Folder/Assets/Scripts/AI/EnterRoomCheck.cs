using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomCheck : MonoBehaviour
{
    public List<BaseSpawner> m_SpawnerList;
    private PlayerPoiseAndHealth m_PlayerStats;
    [SerializeField] private GameObject m_WallWeb;
    [SerializeField] private GameObject m_EndWallWeb;
    private bool m_EnemiesPresent;
    private bool m_EnterRoomResetIsDone;

    private gameManager manager;

    private void Start()
    {
        manager = GameObject.Find("LevelManager").GetComponent<gameManager>();
        StartCoroutine(WaitForSceneLoad());
        
    }
    private void Update()
    {
        
        foreach(BaseSpawner spawner in m_SpawnerList)
        {
            if(spawner.m_EnemiesToBeDeleted.Count > 0 || spawner.m_EnemiesToBeSpawned != 0)
            {
                m_EnemiesPresent = true;
            }
            else
            {
                m_EnemiesPresent = false;
            }
        }

        if(!m_EnemiesPresent)
        {
              m_WallWeb.SetActive(false);
              m_EndWallWeb.SetActive(false);
        }

        if (m_PlayerStats != null && m_PlayerStats.m_IsDead && !m_EnterRoomResetIsDone)
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
            m_EndWallWeb.SetActive(true);
        }
    }

    IEnumerator SpawnerResetDelay()
    {
        yield return new WaitForSeconds(2.2f);
        foreach(BaseSpawner spawner in m_SpawnerList)
        {
            spawner.ResetSpawner();
            //if enemies left > 0
        }

        m_WallWeb.SetActive(false);
        m_EndWallWeb.SetActive(false);
        m_EnterRoomResetIsDone = true;
    }
    IEnumerator WaitForSceneLoad()
    {
        while (!manager.playerScene.isLoaded)
        {
            yield return null;
        }
        if (m_PlayerStats == null)
        {
            m_PlayerStats = GameObject.FindObjectOfType<PlayerPoiseAndHealth>();
        }
        yield return null;
    }
}
