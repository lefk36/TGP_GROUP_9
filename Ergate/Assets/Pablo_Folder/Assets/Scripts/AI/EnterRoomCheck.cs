using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomCheck : MonoBehaviour
{
    public List<BaseSpawner> m_SpawnerList;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach(BaseSpawner spawner in m_SpawnerList)
            {
                spawner.gameObject.SetActive(true);
            }
        }
    }
}
