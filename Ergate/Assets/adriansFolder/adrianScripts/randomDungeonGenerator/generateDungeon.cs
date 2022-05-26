using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateDungeon : MonoBehaviour
{
    public GameObject[] m_listOfRooms;
    private int m_numberOfRooms;
    public int m_maxNumberOfRooms;
    private GameObject[] m_listOfCurrentRooms;
    private roomStats[] m_listOfRoomStats;
    private Vector3 m_newRoomLocation;

    // Start is called before the first frame update
    void Start()
    {
        m_numberOfRooms = Random.Range(1, m_maxNumberOfRooms);
        m_listOfCurrentRooms = new GameObject[m_numberOfRooms];
        m_listOfRoomStats = new roomStats[m_numberOfRooms];
        randomRoomSelection();
        setRoomLocation();
    }

    private void randomRoomSelection()
    {
        for(int i = 0; i < m_listOfCurrentRooms.Length; i++)
        {
            int roomSelection = Random.Range(0, m_listOfRooms.Length);
            m_listOfCurrentRooms[i] = m_listOfRooms[roomSelection];
            m_listOfRoomStats[i] = m_listOfCurrentRooms[i].GetComponent<roomStats>();

        }

    }

    private void setRoomLocation()
    {
        for(int i = 0; i < m_listOfCurrentRooms.Length; i++)
        {
            if(i == 0)
            {
                //Vector3 startLoac = m_listOfRoomStats[i].m_roomExit - m_listOfRoomStats[i].m_roomExit
                Instantiate(m_listOfCurrentRooms[i], m_listOfRoomStats[i].m_roomEnterance, gameObject.transform.rotation);
                m_newRoomLocation = m_listOfRoomStats[i].m_roomEnterance;
            }
            else
            {
                //Vector3 previousPos = m_listOfCurrentRooms[i - 1].transform.position;
                m_newRoomLocation += m_listOfRoomStats[i].m_roomExit;
                Debug.Log("the new room location is: " + m_newRoomLocation);
                Instantiate(m_listOfCurrentRooms[i], m_newRoomLocation, gameObject.transform.rotation);


            }
            

        }

    }

    
}
