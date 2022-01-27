using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{

    [Range(0f, 100f)] //Made it a range so it can be changed in inspector
    [Min(0f)] [SerializeField] private float m_MouseSensitivity; //Mouse Sensitivity of the camera

    [Range(0f, 100f)]
    [Min(0f)] [SerializeField] private float m_ControllerSensitivity; //Controller Sensitivity of the camera

    [Range(0f, 80f)]
    [Min(0f)] [SerializeField] private float m_ClampAngle;//Limit angle movement X rotation
    
    //Mouse Input Variables
    private float m_MouseX;
    private float m_MouseY;
    //Controller Input Variables
    private float m_ControllerHorizontal;
    private float m_ControllerVertical;

    //Mixed Input and rotation
    private float m_MixedInputX;
    private float m_MixedInputY;

    private float m_MixedRotationX;
    private float m_MixedRotationY;
    //Initial Rotation
    private Vector3 m_InitialRotation;
    
    

    private void Start()
    {
        //Makes the camera start on that rotation
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
       
    }
    private void OnEnable()
    {
        //vector of the camera holder rotation
        m_InitialRotation = transform.localRotation.eulerAngles;
        //Set the x and y rotation to the variables made for that
        m_MixedRotationX = m_InitialRotation.x;
        m_MixedRotationY = m_InitialRotation.y;

        //Lock the cursor to not make the camera go around crazy
        Cursor.lockState = CursorLockMode.Locked;
        //Makes the cursor invisible
        Cursor.visible = false;
    }


    private void LateUpdate()
    {
        //Intent camera
        //Sets the Axis input to their variables
        m_ControllerHorizontal = Input.GetAxis("ControllerHorizontal");
        m_ControllerVertical = Input.GetAxis("ControllerVertical");
        m_MouseX = Input.GetAxis("Mouse X");
        m_MouseY = Input.GetAxis("Mouse Y");

        m_MixedInputY = -m_MouseY + m_ControllerVertical;
        m_MixedInputX = m_MouseX + m_ControllerHorizontal;

        //Sets the rotations to the input multiplied by the sensitivity to control how fast the camera moves
        m_MixedRotationY += m_MixedInputX * m_MouseSensitivity * m_ControllerSensitivity * Time.deltaTime;
        m_MixedRotationX += m_MixedInputY * m_MouseSensitivity * m_ControllerSensitivity * Time.deltaTime;

        //Limits the rotation on the x axis
        m_MixedRotationX = Mathf.Clamp(m_MixedRotationX, -m_ClampAngle, m_ClampAngle);


        if (Mathf.Abs(m_MixedInputX) > 0f || Mathf.Abs(m_MixedInputY) > 0)
        {
            transform.localRotation = Quaternion.Euler(m_MixedRotationX, m_MixedRotationY, 0f);
        }
    }

}
