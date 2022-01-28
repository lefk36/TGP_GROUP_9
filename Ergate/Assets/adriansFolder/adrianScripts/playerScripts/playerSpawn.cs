using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    public Vector3 m_spawnLocation;

    public void setSpawnLoc(Vector3 spawnLoc)
    {
        m_spawnLocation = spawnLoc;
        Debug.Log("your new spawn location is: " + m_spawnLocation);
    }

    

    public void onDeath()
    {
        transform.position = m_spawnLocation;
        gameObject.GetComponent<playerStats>().m_health = gameObject.GetComponent<playerStats>().m_maxHealth;
    }
}
