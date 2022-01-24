using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{

    [Range(0f, 2000f)] //Made it a range so it can be changed in inspector
    [Min(0f)] [SerializeField] private float m_MouseSensitivity; //Mouse Sensitivity of the camera

    [Range(0f, 2000f)]
    [Min(0f)] [SerializeField] private float m_ControllerSensitivity; //Controller Sensitivity of the camera

    [Range(0f, 80f)]
    [Min(0f)] [SerializeField] private float m_ClampAngle;//Limit angle movement X rotation
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
    //Initial Rotation
    private Vector3 m_InitialRotation;
    //bool to
    private bool m_UsingController;
    

    private void Start()
    {
        //Makes the camera start on that rotation
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
       
    }
    private void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        //vector of the camera holder rotation
        m_InitialRotation = transform.localRotation.eulerAngles;
        //Set the x and y rotation to the variables made for that
        m_MouseRotationX = m_InitialRotation.x;
        m_MouseRotationY = m_InitialRotation.y;

        m_ControllerRotationX = m_InitialRotation.x;
        m_ControllerRotationY = m_InitialRotation.y;

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

        //Sets the rotations to the input multiplied by the sensitivity to control how fast the camera moves
        m_MouseRotationY += m_MouseX * m_MouseSensitivity * Time.deltaTime;
        m_MouseRotationX += -m_MouseY * m_MouseSensitivity * Time.deltaTime;

        m_ControllerRotationY += m_ControllerHorizontal * m_ControllerSensitivity * Time.deltaTime;
        m_ControllerRotationX += m_ControllerVertical * m_ControllerSensitivity * Time.deltaTime;

        //Limits the rotation on the x axis
        m_MouseRotationX = Mathf.Clamp(m_MouseRotationX, -m_ClampAngle, m_ClampAngle);
        m_ControllerRotationX = Mathf.Clamp(m_ControllerRotationX, -m_ClampAngle, m_ClampAngle);
        
        Debug.Log("Mouse Input Y" + m_MouseRotationX);
        Debug.Log("Mouse Input X" + m_MouseRotationY);
        Debug.Log("Controller Input Y" + m_ControllerRotationX);
        Debug.Log("Controller Input X" + m_ControllerRotationY);

        if (Mathf.Abs(m_ControllerRotationX) > 0f || Mathf.Abs(m_ControllerRotationY) > 0f)
        {
            
            if(m_UsingController)
            {
                m_MouseRotationX = 0f;
                m_MouseRotationY = 0f;
                m_UsingController = false;
            }
            
            transform.rotation = Quaternion.Euler(m_ControllerRotationX, m_ControllerRotationY, 0f);
            

        }
        
        //Sets the rotation of the camera holder to the rotation on the x and y axis depending on if the player is using the mouse or a controller 
         
        if (Mathf.Abs(m_MouseRotationX) > 0f || Mathf.Abs(m_MouseRotationY) > 0f)
        { 
           
           if(!m_UsingController)
           {
                m_ControllerRotationX = 0f;
                m_ControllerRotationY = 0f;
                m_UsingController = true;
           }
            
           transform.rotation = Quaternion.Euler(m_MouseRotationX, m_MouseRotationY, 0f);
            
            
        }
       

    }

}
