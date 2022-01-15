using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Vector3 m_LocalCameraOffSet; //Position of the camera from the player.
    [SerializeField] private Transform m_TrackedObjectTransform; //Object that the camera will follow
    [SerializeField] private Transform m_Camera; //Camera Transform
    [SerializeField] private float m_AutoCamSpeed; //Speed in which the camra follows the player
    [SerializeField] private float m_Sensitivity; //Sensitivity of the camera
    [SerializeField] private float m_ClampAngle;//Limit angle movement X rotation
    //Mouse Input Variables
    private float m_MouseX;
    private float m_MouseY;
    //Controller Input Variables
    private float m_ControllerHorizontal;
    private float m_ControllerVertical;
    //Input of both mouse and Controller
    private float m_MixedInputX;
    private float m_MixedInputY;
    //Rotation X axis
    private float m_RotationX;
    //ROtation Y Axis
    private float m_RotationY;

    private void Start()
    {
        //vector of the camera holder rotation
        Vector3 rotation = transform.localRotation.eulerAngles;
        //Set the x and y rotation to the variables made for that
        m_RotationX = rotation.x;
        m_RotationY = rotation.y;
        //Lock the cursor to not make the camera go around crazy
        Cursor.lockState = CursorLockMode.Locked;
        //Makes the cursor invisible
        Cursor.visible = false;

    }

    private void Update()
    {
        //Intent camera
        //Sets the Axis input to their variables
        m_ControllerHorizontal = Input.GetAxis("ControllerHorizontal");
        m_ControllerVertical = Input.GetAxis("ControllerVertical");
        m_MouseX = Input.GetAxis("Mouse X");
        m_MouseY = Input.GetAxis("Mouse Y");

        //Mixes both mouse and controller input, so I dont put the code twice
        m_MixedInputX = m_MouseX + m_ControllerHorizontal;
        m_MixedInputY = -m_MouseY + m_ControllerVertical;

        //Sets the rotations to the input multiplied by the sensitivity to control how fast the camera moves
        m_RotationY += m_MixedInputX * m_Sensitivity * Time.deltaTime;
        m_RotationX += m_MixedInputY * m_Sensitivity * Time.deltaTime;

        //Limits the rotation on the x axis
        m_RotationX = Mathf.Clamp(m_RotationX, -m_ClampAngle, m_ClampAngle);

        //Sets the rotation of the camera holder to the rotation on the x and y axis
        transform.rotation = Quaternion.Euler(m_RotationX, m_RotationY, 0f);
    }

    //I put it on FixedUpdate because in Late it makes the camera jittery
    private void FixedUpdate()
    {

        //AutoCamera
        float worldYRotInRad = m_Camera.rotation.eulerAngles.y * Mathf.Deg2Rad; //The y rotation of the camera in radians

        //Current position of the camera
        Vector3 currentCameraPosition = m_Camera.position - new Vector3(m_LocalCameraOffSet.z * Mathf.Sin(worldYRotInRad) + m_LocalCameraOffSet.x * Mathf.Cos(worldYRotInRad),
                                                                    m_LocalCameraOffSet.y,
                                                                    m_LocalCameraOffSet.z * Mathf.Cos(worldYRotInRad) - m_LocalCameraOffSet.x * Mathf.Sin(worldYRotInRad));
        //Position of the offSetCamera
        Vector3 cameraOffSetPosition = m_Camera.position - currentCameraPosition;

        //Distance from the camera to the target
        float distToTarget = (m_TrackedObjectTransform.position - currentCameraPosition).magnitude;

        //Makes the camera follow the player
        m_Camera.position = Vector3.MoveTowards(currentCameraPosition, m_TrackedObjectTransform.position, distToTarget * m_AutoCamSpeed * Time.fixedDeltaTime) + cameraOffSetPosition;

        //Intent Camera
        //Modify the rotation of the y rotation of the rotation point in the character. 
        //if (Input.GetAxis("Mouse X") > 0f)
        //{
        //    //Positive X axis
        //    m_RotationPoint.rotation = m_RotationPoint.rotation * Quaternion.Euler(0f, Input.GetAxis("Mouse X") * m_Sensitivity * Time.fixedDeltaTime, 0f);
        //}
        //else if (Input.GetAxis("Mouse X") < 0f)
        //{
        //    //Negative x axis
        //    m_RotationPoint.rotation = m_RotationPoint.rotation * Quaternion.Euler(0f, Input.GetAxis("Mouse X") * m_Sensitivity * Time.fixedDeltaTime, 0f);
        //}
        //else if (Input.GetAxis("ControllerHorizontal") > 0)
        //{
        //    //Controller positive x axis
        //    m_RotationPoint.rotation = m_RotationPoint.rotation * Quaternion.Euler(0f, Input.GetAxis("ControllerHorizontal") * m_Sensitivity * Time.fixedDeltaTime, 0f);
        //}
        //else if (Input.GetAxis("ControllerHorizontal") < 0)
        //{
        //    //Controller negative x axis
        //    m_RotationPoint.rotation = m_RotationPoint.rotation * Quaternion.Euler(0f, Input.GetAxis("ControllerHorizontal") * m_Sensitivity * Time.fixedDeltaTime, 0f);
        //}


    }


}
