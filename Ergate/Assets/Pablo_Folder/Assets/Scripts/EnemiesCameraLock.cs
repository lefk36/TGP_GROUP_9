using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCameraLock : MonoBehaviour
{
    //Variable that will hold the input of the scrollwheel
    private float m_ScrollWheelInputValue = 0;
    //Index of the enemies in the targetable enemies list
    private int m_TargetableEnemyIndex = 0;

    private bool m_ControllerHorizontalInputDone = false;

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
    //Render of the player
    [SerializeField] private Renderer m_Player;
    //Vector between camera and enemy
    private Vector3 m_RayDirection;
    //Bool to set camera movement to true or false
    private bool m_CameraMovementActive = true;

    private void Start()
    {
        //Coroutine that happens every 0.1 seconds
        StartCoroutine(EnemyListChange());    
    }

    private void Update()
    {
        //Holders of the input that we are going to use for the camera lock
        m_ScrollWheelInput = Input.mouseScrollDelta.y;
        m_ControllerHorizontal = Input.GetAxis("ControllerHorizontal");
        m_RightStickPressed = Input.GetButtonDown("RightStick");
        m_MiddleButtonPressed = Input.GetMouseButtonDown(2);

        //If the player press the mouse middle button or presses the right stick
        if (m_MiddleButtonPressed || m_RightStickPressed)
        {
            //If the lock mode is not active
            if (!m_LockOn)
            {
                //If the list of targetable enemies is different than 0 then set the lock mode to true
                if (m_TargetableEnemies.Count != 0)
                {
                    m_LockOn = true;
                }
            }
            else if (m_LockOn)
            {
                //If the lock mode is active set it to false
                m_LockOn = false;
            }



        }

        //If the lock mode is active
        if (m_LockOn)
        {
            //Mouse Controller
            //Sets a value for the scrollwheel input
            m_ScrollWheelInputValue += m_ScrollWheelInput;
            //If the camera movement is active, then disenable it
            if (m_CameraMovementActive)
            {
                m_CameraMovement.enabled = false;
                m_CameraMovementActive = false;
            }

            //If the scrollWheel input is not 0 means that we go up or down on the targetable enemies list
            if (m_ScrollWheelInputValue != 0f)
            {
                //If you scroll up go up in the list
                if (m_ScrollWheelInputValue > 0f)
                {
                    m_TargetableEnemyIndex++;
                    //If the index is greater than the last index in the list set it to 0
                    if (m_TargetableEnemyIndex > m_TargetableEnemies.Count - 1)
                    {
                        m_TargetableEnemyIndex = 0;
                    }
                    m_ScrollWheelInputValue = 0f;

                }
                //if you scroll down go down in the list
                else if (m_ScrollWheelInputValue < 0f)
                {
                    m_TargetableEnemyIndex--;
                    //If the index is less than 0 then go to the las index in the list
                    if (m_TargetableEnemyIndex < 0)
                    {
                        m_TargetableEnemyIndex = m_TargetableEnemies.Count - 1;
                    }
                    m_ScrollWheelInputValue = 0f;
                }

                
            }

            if(m_ControllerHorizontal > 0f && !m_ControllerHorizontalInputDone)
            {
                
                m_ControllerHorizontalInputDone = true;
                m_TargetableEnemyIndex++;
                //If the index is greater than the last index in the list set it to 0
                if (m_TargetableEnemyIndex > m_TargetableEnemies.Count - 1)
                {
                    m_TargetableEnemyIndex = 0;
                }

                
                
            }
            else if(m_ControllerHorizontal < 0f && !m_ControllerHorizontalInputDone)
            {
                m_ControllerHorizontalInputDone = true;
                m_TargetableEnemyIndex--;
                //If the index is less than 0 then go to the las index in the list
                if (m_TargetableEnemyIndex < 0)
                {
                    m_TargetableEnemyIndex = m_TargetableEnemies.Count - 1;
                }
                
            }

            if (m_ControllerHorizontal == 0)
            {
                m_ControllerHorizontalInputDone = false;
            }

            //If there is only 1 item in the list, the index if the list is 0
            if (m_TargetableEnemies.Count == 1)
            {
                m_TargetableEnemyIndex = m_TargetableEnemies.Count - 1;
            }

            //If the player is not visible the lock mode turns off
            if (!m_Player.isVisible)
            {
                m_LockOn = false;
            }

            //Sets the object to look at and the smooth rotation of the camera center in Y
            m_TargetToLook.targetObj = m_TargetableEnemies[m_TargetableEnemyIndex].transform;
            Vector3 cameraCenterToEnemy = m_TargetableEnemies[m_TargetableEnemyIndex].transform.position - m_CameraMovement.gameObject.transform.position;
            Quaternion lookEnemyRotation = Quaternion.LookRotation(cameraCenterToEnemy);
            Vector3 rotation = Quaternion.Lerp(m_CameraMovement.gameObject.transform.rotation, lookEnemyRotation, Time.deltaTime * m_TurnToEnemySpeed).eulerAngles;
            m_CameraMovement.gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        else
        {
            //If the lock mode is off, then the camera comes back to the initial position and the camera movement gets enable
            m_TargetToLook.targetObj = null;
            if (!m_CameraMovementActive)
            {
                m_CameraMovement.enabled = true;
                m_CameraMovementActive = true;
            }
        }
    }

    //Coroutine at the start
    IEnumerator EnemyListChange()
    {
        //While the game is running
        while (true)
        {
            //Gets all the enemies in the game and puts them in an array
            m_Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //Foreach enemy in the array
            foreach (GameObject enemy in m_Enemies)
            {
                //Creates raycasts from the camera to all the enemies
                m_RayDirection = enemy.transform.position - transform.position;
                m_Hits = Physics.RaycastAll(transform.position, m_RayDirection, m_MaxDistanceAllowed, m_Layer);
                //If the enemies are visible
                if (enemy.GetComponent<Renderer>().isVisible)
                {
                    //Foreach hit in the raycasts crated if the tag of the collider is enemy, then add that gameObject in the list if it wasnt before
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
                    //If the enemies are not visible
                    foreach (RaycastHit hit in m_Hits)
                    {
                        //Gets the index of the object/s that are about to be removed and removes them
                        int indexOfGameObjectToRemove = m_TargetableEnemies.FindIndex(x => x.Equals(hit.collider.gameObject));
                        m_TargetableEnemies.Remove(hit.collider.gameObject);

                        //Updates the List and theindex of the enemies
                        //If the index is greater than the last index, then the index is the last index in the list
                        if(m_TargetableEnemyIndex > m_TargetableEnemies.Count - 1)
                        {
                            m_TargetableEnemyIndex = m_TargetableEnemies.Count - 1;
                        }
                        //If the index is less than 0, then it sets the index to 0
                        else if(m_TargetableEnemyIndex < 0)
                        {
                            m_TargetableEnemyIndex = 0;
                        }
                        //If the index is equal to the index that is about to be removed from screen, it sets the index to that one minus 1
                        else if(m_TargetableEnemyIndex == indexOfGameObjectToRemove)
                        {
                            m_TargetableEnemyIndex = indexOfGameObjectToRemove - 1;
                        }

                    }
                }

            }
            //It happens every 0.1 seconds
            yield return new WaitForSeconds(0.1f);
        }
    }
}
