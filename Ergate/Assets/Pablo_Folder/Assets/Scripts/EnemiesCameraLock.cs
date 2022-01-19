using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCameraLock : MonoBehaviour
{
    //Bool to tell if the lock is on or off
    public bool m_LockOn;
    //Array of every hit that happens
    private RaycastHit[] m_Hits;
    //Variable that will take the scrollwheel input
    private float m_ScrollWheelInput;
    //Variable that will take the horizontal input of the controller
    private float m_ControllerHorizontal;
    //Variable that will take the input for the right Stick press
    private bool m_RightStickPressed;
    //Variable that will take the input of the middle button click
    private bool m_MiddleButtonPressed;
    //Maximum distance range
    [SerializeField] private float m_MaxDistanceAllowed;
    
    //List of enemies
    private GameObject[] m_Enemies;
    //List of enemies located
    List<GameObject> m_TargetableEnemies = new List<GameObject>();
    //LayerMask for the enemies
    [SerializeField] private LayerMask m_Layer;
    //Target that the camera will be looking at
    [SerializeField] private SimpleLookAt m_TargetToLook;
    //Camera Center
    [SerializeField] private Camera_Movement m_CameraMovement;
    private Vector3 m_RayDirection;

    private void Start()
    {
        StartCoroutine(EnemyListChange());

    }

    private void Update()
    {
        
        m_ScrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        m_ControllerHorizontal = Input.GetAxis("ControllerHorizontal");
        m_RightStickPressed = Input.GetButton("RightStick");
        m_MiddleButtonPressed = Input.GetMouseButtonDown(2);

        //m_Hits = Physics.SphereCastAll(transform.position, m_SphereCastRadius, Vector3.forward, m_MaxDistanceAllowed, m_Enemy);

        //for(int i = 0; i < m_Hits.Length; i++)
        //{
        //   if(Physics.Linecast(transform.position, enemy.transform.position, out m_Hits[i]))
        //   {
        //        Debug.DrawLine(transform.position, m_Hits[i].point, Color.red);
        //   }
        //}

        //Ray CameraToEnemyRay = m_Camera.ScreenPointToRay(enemy.transform.position);
        //Debug.Log(CameraToEnemyRay);
        //Debug.DrawLine(transform.position, enemy.transform.position, Color.red);
        //m_Hits = Physics.RaycastAll(CameraToEnemyRay, m_MaxDistanceAllowed, m_Enemy);
        //for(int i = 0; i < m_Hits.Length; i++)
        //{
        //    if(Physics.Raycast(CameraToEnemyRay, out m_Hits[i], m_MaxDistanceAllowed, m_Enemy))
        //    {
        //        Debug.DrawLine(transform.position, m_Hits[i].point, Color.green);
        //    }
        //}

        if (m_MiddleButtonPressed || m_RightStickPressed)
        {
            if(m_TargetableEnemies.Count != 0)
            {
                m_TargetToLook.target = m_TargetableEnemies[0].transform;
                m_LockOn = true;

                

            }
        }

        
    }

    IEnumerator EnemyListChange()
    {
        while(true)
        {
            m_Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            foreach (GameObject enemy in m_Enemies)
            {
                m_RayDirection = enemy.transform.position - transform.position;
                m_Hits = Physics.RaycastAll(transform.position, m_RayDirection, m_MaxDistanceAllowed, m_Layer);
                Debug.DrawRay(transform.position, m_RayDirection, Color.red);
                if (enemy.GetComponent<Renderer>().isVisible)
                {
                   
                    foreach (RaycastHit hit in m_Hits)
                    {
                        
                        if (hit.collider.tag == "Enemy")
                        {
                            if (!m_TargetableEnemies.Contains(hit.collider.gameObject))
                            {
                                m_TargetableEnemies.Add(hit.collider.gameObject);
                            }
                        }
                    }
                }
                else
                {
                    foreach(RaycastHit hit in m_Hits)
                    {
                        m_TargetableEnemies.Remove(hit.collider.gameObject);
                        Debug.Log("Object " + hit.collider.gameObject + " removed from list");
                    }
                }
                
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
