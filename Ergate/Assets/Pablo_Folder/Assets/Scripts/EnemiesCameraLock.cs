using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCameraLock : MonoBehaviour
{

    private float m_CameraTargetIndex = 0;
    [SerializeField] private int m_TargetableEnemyIndex = 0;

    //Bool to tell if the lock is on or off
    public bool m_LockOn;
    //Array of every hit that happens
    private RaycastHit[] m_Hits;
    //Variable that will take the scrollwheel input
    private float m_ScrollWheelInput;
    //Variable that will take the horizontal input of the controller
    private float m_ControllerHorizontal;
    //Turn speed of the camera
    [SerializeField] private float m_TurnToEnemySpeed;
    //Variable that will take the input for the right Stick press
    private bool m_RightStickPressed;
    //Variable that will take the input of the middle button click
    private bool m_MiddleButtonPressed;
    //Maximum distance range
    [SerializeField] private float m_MaxDistanceAllowed;

    //List of enemies
    private GameObject[] m_Enemies;
    //List of enemies located
    [SerializeField] private List<GameObject> m_TargetableEnemies = new List<GameObject>();
    //LayerMask for the enemies
    [SerializeField] private LayerMask m_Layer;
    //Target that the camera will be looking at
    [SerializeField] private SimpleLookAt m_TargetToLook;
    //Camera Center
    [SerializeField] private Camera_Movement m_CameraMovement;
    [SerializeField] private Renderer m_Player;
    private Vector3 m_RayDirection;

    private bool m_CameraMovementActive = true;

    private void Start()
    {
        StartCoroutine(EnemyListChange());    
    }

    private void Update()
    {
        
        m_ScrollWheelInput = Input.mouseScrollDelta.y;
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
            if (!m_LockOn)
            {
                if (m_TargetableEnemies.Count != 0)
                {
                    m_LockOn = true;
                }
            }
            else if (m_LockOn)
            {
                m_LockOn = false;
            }



        }

        if (m_LockOn)
        {
            m_CameraTargetIndex += m_ScrollWheelInput;

            if (m_CameraMovementActive)
            {
                m_CameraMovement.enabled = false;
                m_CameraMovementActive = false;
            }

            if (m_CameraTargetIndex != 0f)
            {
                if (m_CameraTargetIndex > 0f)
                {
                    m_TargetableEnemyIndex++;
                    if (m_TargetableEnemyIndex > m_TargetableEnemies.Count - 1)
                    {
                        m_TargetableEnemyIndex = 0;
                    }
                    m_CameraTargetIndex = 0f;

                }
                else if (m_CameraTargetIndex < 0f)
                {
                    m_TargetableEnemyIndex--;
                    if (m_TargetableEnemyIndex < 0)
                    {
                        m_TargetableEnemyIndex = m_TargetableEnemies.Count - 1;
                    }
                    m_CameraTargetIndex = 0f;
                }
            }

            if(!m_Player.isVisible)
            {
                m_LockOn = false;
            }
            

            m_TargetToLook.targetObj = m_TargetableEnemies[m_TargetableEnemyIndex].transform;
            Vector3 cameraCenterToEnemy = m_TargetableEnemies[m_TargetableEnemyIndex].transform.position - m_CameraMovement.gameObject.transform.position;
            Quaternion lookEnemyRotation = Quaternion.LookRotation(cameraCenterToEnemy, Vector3.up);
            Vector3 rotation = Quaternion.Lerp(m_CameraMovement.gameObject.transform.rotation, lookEnemyRotation, Time.deltaTime * m_TurnToEnemySpeed).eulerAngles;
            
            m_CameraMovement.gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        else
        {
            m_TargetToLook.targetObj = null;
            if (!m_CameraMovementActive)
            {
                m_CameraMovement.enabled = true;
                m_CameraMovementActive = true;
            }
        }
    }
    IEnumerator EnemyListChange()
    {
        while (true)
        {
            m_Enemies = GameObject.FindGameObjectsWithTag("Enemy");

            Debug.Log("CurrentIndexOnTarget" + m_TargetableEnemyIndex);
            Debug.Log("ListLength" + m_TargetableEnemies.Count);
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
                    foreach (RaycastHit hit in m_Hits)
                    {

                        int indexOfGameObjectToRemove = m_TargetableEnemies.FindIndex(x => x.Equals(hit.collider.gameObject));
                        Debug.Log("IndexToRemove" + indexOfGameObjectToRemove);
                        m_TargetableEnemies.Remove(hit.collider.gameObject);

                        if(m_TargetableEnemyIndex > m_TargetableEnemies.Count - 1)
                        {
                            m_TargetableEnemyIndex = m_TargetableEnemies.Count - 1;
                        }
                        else if(m_TargetableEnemyIndex < 0)
                        {
                            m_TargetableEnemyIndex = 0;
                        }
                        else if(m_TargetableEnemyIndex == indexOfGameObjectToRemove)
                        {
                            m_TargetableEnemyIndex = indexOfGameObjectToRemove - 1;
                        }
                        
                        //if (indexOfGameObjectToRemove >= 0)
                        //{
                            
                        //    //if (indexOfGameObjectToRemove > m_TargetableEnemyIndex)
                        //    //{
                        //        m_TargetableEnemyIndex = 0;
                        //    //}
                        //}



                        //else if (indexOfGameObjectToRemove == m_TargetableEnemyIndex)
                        //{
                        //    m_LockOn = false;
                        //}

                        //if (indexOfGameObjectToRemove >= 0)
                        //{

                        //}



                        //var test = m_TargetableEnemies[indexOfGameObjectToRemove];
                        //m_TargetableEnemies.Add(test);

                    }
                }

            }




            yield return new WaitForSeconds(0.1f);
        }
    }
}
