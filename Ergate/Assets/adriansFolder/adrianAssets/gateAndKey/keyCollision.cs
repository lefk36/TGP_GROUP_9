using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCollision : MonoBehaviour
{
    private GameObject m_player;
    public keyStats m_keyStats;
    private int m_keyID, m_keyInstance;

    private void Start()
    {

        
        m_player = FindObjectOfType<PlayerController>().gameObject;
        m_keyID = m_keyStats.itemID;
        m_keyInstance = m_keyStats.keyInstance;

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        m_player.GetComponent<playerInventory>().addItem(m_keyID, m_keyInstance);
        Destroy(gameObject);
    }
    
}
