using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCheckpoint : MonoBehaviour
{
    //public Vector3 m_spawnLocation;    
    private GameObject m_player;
    public GameObject m_spawnLoc;

    private void Start()
    {
        m_player = FindObjectOfType<PlayerController>().gameObject;
        //m_spawnLocation = transform.position;
    }

    public void setSpawnLocation(GameObject player)
    {
        m_player = player;
        m_player.GetComponent<playerSpawn>().setSpawnLoc(m_spawnLoc.transform.position);
        Debug.Log("spawn location set");

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            setSpawnLocation(other.gameObject);
        }
    }

    public void rest()
    {
        //reset health and reset the all the enemies except bosses
    }
}
