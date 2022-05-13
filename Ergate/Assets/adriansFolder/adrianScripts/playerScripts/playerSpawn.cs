using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    public Vector3 m_spawnLocation;
    private GameObject m_Character;
    private Animator m_Animator;
    [HideInInspector] public GameObject m_Model;
    private PlayerPoiseAndHealth m_PlayerStats;
    private PlayerController m_PlayerController;
    private EnemiesCameraLock m_CameraLock;
    private GameObject m_CameraCenter;
    private GameObject m_MainCamera;


    private void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
        m_PlayerStats = GetComponent<PlayerPoiseAndHealth>();
        m_Character = transform.Find("Character").gameObject;
        m_CameraCenter = transform.Find("Camera Centre").gameObject;

        if(m_CameraCenter != null)
        {
            m_MainCamera = m_CameraCenter.transform.Find("Main Camera").gameObject;
            if(m_MainCamera != null)
            {
                m_CameraLock = m_MainCamera.GetComponent<EnemiesCameraLock>();
            }
        }
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
        if(m_PlayerStats.m_IsDead)
        {
            StartCoroutine(PlayerRespawnDelay());
        }
    }

   
    IEnumerator PlayerRespawnDelay()
    {
        yield return new WaitForSeconds(2.2f);
        m_CameraLock.m_LockOn = false;
        m_PlayerStats.m_currentPlayerHealth = m_PlayerStats.m_maximumHealth;
        m_PlayerStats.m_currentPlayerPoise = m_PlayerStats.m_maximumPoise;
        m_PlayerController.lockMovement = false;        //stuns the player while they're DEAD
        m_PlayerController.lockAttackDirection = false; //
        m_PlayerController.readyForAction = true;
        transform.position = m_spawnLocation;
        m_Animator.SetTrigger("GettingUp");
        m_PlayerStats.m_IsDead = false;
        Debug.Log("Coroutine done");
        
    }
}
