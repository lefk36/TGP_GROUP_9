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
        gameObject.GetComponent<PlayerPoiseAndHealth>().m_currentPlayerHealth = gameObject.GetComponent<PlayerPoiseAndHealth>().m_maximumHealth;
        gameObject.GetComponent<PlayerPoiseAndHealth>().m_currentPlayerPoise = gameObject.GetComponent<PlayerPoiseAndHealth>().m_maximumPoise;

        gameObject.GetComponent<PlayerController>().lockMovement = false;        //stuns the player while they're DEAD
        gameObject.GetComponent<PlayerController>().lockAttackDirection = false; //
        gameObject.GetComponent<PlayerController>().readyForAction = true;
    }
}
