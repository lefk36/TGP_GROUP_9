using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerConToAdd : MonoBehaviour
{
    //layermask for interaction
    public LayerMask m_checkpointMask;
    public LayerMask m_doorMask;

    private GameObject m_doorToOpen;
    public GameObject m_player;
    public Transform camera;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 10f);
        Gizmos.DrawRay(transform.position, camera.forward);
    }

    private void interact()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.SphereCast(gameObject.transform.position, 5f, camera.forward, out hit, 5f, m_checkpointMask))
            {
                
                Debug.Log("did hit the checkpoint");
                hit.transform.gameObject.GetComponent<playerCheckpoint>().setSpawnLocation(m_player);
                
            }
            else
            {
                Debug.Log("did not hit the checkpoint");
            }

            if (Physics.Raycast(transform.position, camera.forward, out hit, Mathf.Infinity, m_doorMask))
            {
                m_doorToOpen = hit.collider.gameObject;
                m_doorToOpen.GetComponent<gateScript>().openGate();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.GetComponent<playerStats>().takeDamage(10f);

        }
    }

    
    private void Update()
    {
        interact();
    }

}
