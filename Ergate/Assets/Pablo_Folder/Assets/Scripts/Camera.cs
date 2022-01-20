using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    [Range(0f, 2000f)] //Made it a range so it can be changed in inspector
    [Min(0f)][SerializeField] private float m_MouseSensitivity; //Mouse Sensitivity of the camera

    [Range(0f, 2000f)]
    [Min(0f)] [SerializeField] private float m_ControllerSensitivity; //Controller Sensitivity of the camera

    [Range(0f, 80f)]
    [Min(0f)][SerializeField] private float m_ClampAngle;//Limit angle movement X rotation
    //Mouse Input Variables
    private float m_MouseX;
    private float m_MouseY;
    //Controller Input Variables
    private float m_ControllerHorizontal;
    private float m_ControllerVertical;
    //Rotation X axis
    private float m_MouseRotationX;
    //ROtation Y Axis
    private float m_MouseRotationY;
    //Rotation X axis(Controller)
    private float m_ControllerRotationX;
    //ROtation Y Axis(Controller)
    private float m_ControllerRotationY;
    //List of enemies located
    List<GameObject> m_EnemiesLocated = new List<GameObject>();
    private void OnEnable()
    {
        //vector of the camera holder rotation
        Vector3 rotation = transform.localRotation.eulerAngles;
        //Set the x and y rotation to the variables made for that
        m_MouseRotationX = rotation.x;
        m_MouseRotationY = rotation.y;

        m_ControllerRotationX = rotation.x;
        m_ControllerRotationY = rotation.y;

        //Lock the cursor to not make the camera go around crazy
        Cursor.lockState = CursorLockMode.Locked;
        //Makes the cursor invisible
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        //on disabled, make cursor work as normal
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void LateUpdate()
    {
        //Intent camera
        //Sets the Axis input to their variables
        m_ControllerHorizontal = Input.GetAxis("ControllerHorizontal");
        m_ControllerVertical = Input.GetAxis("ControllerVertical");
        m_MouseX = Input.GetAxis("Mouse X");
        m_MouseY = Input.GetAxis("Mouse Y");
        

        //Sets the rotations to the input multiplied by the sensitivity to control how fast the camera moves
        m_MouseRotationY += m_MouseX * m_MouseSensitivity * Time.deltaTime;
        m_MouseRotationX += -m_MouseY * m_MouseSensitivity * Time.deltaTime;

        m_ControllerRotationY += m_ControllerHorizontal * m_ControllerSensitivity * Time.deltaTime;
        m_ControllerRotationX += m_ControllerVertical * m_ControllerSensitivity * Time.deltaTime;

        //Limits the rotation on the x axis
        m_MouseRotationX = Mathf.Clamp(m_MouseRotationX, -m_ClampAngle, m_ClampAngle);
        m_ControllerRotationX = Mathf.Clamp(m_ControllerRotationX, -m_ClampAngle, m_ClampAngle);

        //Sets the rotation of the camera holder to the rotation on the x and y axis depending on if the player is using the mouse or a controller
        if(Input.GetAxis("Mouse X") < 0.1f || Input.GetAxis("Mouse X") > 0.1f || Input.GetAxis("Mouse Y") < 0.1f || Input.GetAxis("Mouse Y") > 0.1f)
        {
            transform.rotation = Quaternion.Euler(m_MouseRotationX, m_MouseRotationY, 0f);
        }
        else if(Input.GetAxis("ControllerHorizontal") < 0.1f || Input.GetAxis("ControllerHorizontal") > 0.1f || Input.GetAxis("ControllerVertical") < 0.1f || Input.GetAxis("ControllerVertical") > 0.1f)
        {
            transform.rotation = Quaternion.Euler(m_ControllerRotationX, m_ControllerRotationY, 0f);
        }
        
        
    }

    ////I put it on FixedUpdate because in Late it makes the camera jittery
    //private void FixedUpdate()
    //{
    //    //AutoCamera
    //    float worldYRotInRad = m_Camera.rotation.eulerAngles.y * Mathf.Deg2Rad; //The y rotation of the camera in radians
    //    //Current position of the camera
    //    Vector3 currentCameraPosition = m_Camera.position - new Vector3(m_LocalCameraOffSet.z * Mathf.Sin(worldYRotInRad) + m_LocalCameraOffSet.x * Mathf.Cos(worldYRotInRad),
    //                                                                m_LocalCameraOffSet.y,
    //                                                                m_LocalCameraOffSet.z * Mathf.Cos(worldYRotInRad) - m_LocalCameraOffSet.x * Mathf.Sin(worldYRotInRad));

    //    //Position of the offSetCamera
    //    Vector3 cameraOffSetPosition = m_Camera.position - currentCameraPosition;

    //    //Distance from the camera to the target
    //    float distToTarget = (m_TrackedObjectTransform.position - currentCameraPosition).magnitude;

    //    //Makes the camera follow the player
    //    m_Camera.position = Vector3.MoveTowards(currentCameraPosition, m_TrackedObjectTransform.position, distToTarget * m_AutoCamSpeed * Time.fixedDeltaTime) + cameraOffSetPosition;


    //}


}
