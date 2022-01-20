using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateScript : MonoBehaviour
{
    public GameObject m_key;
    private keyStats m_keyStats;
    private bool m_gateOpen;
    private GameObject m_player;
    private int keyID;
    private int keyInstance;
    private int[] keyCheck;
    private playerInventory m_playerInventory;

    public List<int[]> inventoryToCheck = new List<int[]>();
    

    private void Awake()
    {
        m_player = FindObjectOfType<playerController>().gameObject;
        m_keyStats = m_key.GetComponent<keyStats>();
        keyID = m_keyStats.itemID;
        keyInstance = m_keyStats.keyInstance;
        m_playerInventory = m_player.GetComponent<playerInventory>();
        keyCheck = new int[2];
        keyCheck[0] = keyID;
        keyCheck[1] = keyInstance;




    }

    public void openGate()
    {
        inventoryToCheck = new List<int[]>(m_playerInventory.inventory);
        if(!m_gateOpen)
        {
            //checks through the inventory to see if you have the correct key for the gate
            for(int i = 0; i < inventoryToCheck.Count; i++)
            {
                Debug.Log(i +" " + inventoryToCheck[i][0] + " " + inventoryToCheck[i][1]);
                if(inventoryToCheck[i][0] == keyCheck[0] && inventoryToCheck[i][1] == keyCheck[1])
                {
                    Vector3 newRot = new Vector3(transform.parent.rotation.x, transform.parent.rotation.y + 90f, transform.parent.rotation.z);
                    m_gateOpen = true;
                    transform.parent.RotateAround(transform.parent.position, transform.parent.up, transform.parent.rotation.y + 90f); 
                    break;
                }
            }

            if(m_gateOpen)
            {
                Debug.Log("gate is now open");
            }
            else
            {
                Debug.Log("you do not have the key");

            }
           
        }
        else
        {
            transform.parent.RotateAround(transform.parent.position, transform.parent.up, transform.parent.rotation.y + 90f);
        }
    }
}
