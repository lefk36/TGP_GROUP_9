using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    public Vector3 m_spawnLocation;
    private GameObject m_Character;
    private Animator m_Animator;
    [HideInInspector] public GameObject m_Model;


    private void Start()
    {
        m_Character = transform.Find("Character").gameObject;
        if (m_Character != null)
        {
            m_Model = m_Character.transform.Find("Model").gameObject;
            if (m_Model != null)
            {
                m_Animator = m_Model.GetComponent<Animator>();
            }
            else Debug.Log("No child object with the name 'Model' was found");
        }
    }

    public void setSpawnLoc(Vector3 spawnLoc)
    {
        m_spawnLocation = spawnLoc;
        Debug.Log("your new spawn location is: " + m_spawnLocation);
    }

    

    public void onDeath()
    {
        Debug.Log("Coroutine Runnning");
        StartCoroutine(PlayerRespawnDelay());
    }

    IEnumerator PlayerRespawnDelay()
    {
        yield return new WaitForSeconds(1.25f);
        transform.position = m_spawnLocation;
        gameObject.GetComponent<PlayerPoiseAndHealth>().m_currentPlayerHealth = gameObject.GetComponent<PlayerPoiseAndHealth>().m_maximumHealth;
        gameObject.GetComponent<PlayerPoiseAndHealth>().m_currentPlayerPoise = gameObject.GetComponent<PlayerPoiseAndHealth>().m_maximumPoise;
        gameObject.GetComponent<PlayerController>().lockMovement = false;        //stuns the player while they're DEAD
        gameObject.GetComponent<PlayerController>().lockAttackDirection = false; //
        gameObject.GetComponent<PlayerController>().readyForAction = true;
    }
}
