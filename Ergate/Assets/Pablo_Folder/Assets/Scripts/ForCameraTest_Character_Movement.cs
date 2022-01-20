using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCameraTest_Character_Movement : MonoBehaviour
{
    private Rigidbody m_RB; //Rigidbody
    [SerializeField] private CapsuleCollider m_PlayerCollider; //Collider of the player
    [SerializeField] private LayerMask m_Ground; //Ground layr mask
    [SerializeField] private Transform m_Cam;

    [SerializeField] [Min(0f)] private float m_TurnSmoothTime; //Makes the rotation of the player smoother
    private float m_PlayerSpeed = 1f; //Speed of the player
    private float m_RotationSpeed; //How fast the player rotates
    private float m_MaxSpeed = 8f; //Maximum speed to cap the velociy
    private float m_StopingForce = 10f; //Force to stop the player from moving
    private bool m_IsGrounded; //Bool to know if the player is grounded or not
    private int m_JumpCount = 0;

    private float m_JumpForce = 7f;

    private void Awake()
    {
        m_RB = GetComponent<Rigidbody>();
        

    }

    private void Update()
    {
        if (m_JumpCount == 0 && Input.GetButtonDown("Jump"))
        {
            m_RB.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            
            m_JumpCount = 1;
            Debug.Log(m_JumpCount);

        }

        if (!IsGrounded() && m_JumpCount == 1 && Input.GetButtonDown("Jump"))
        {

            m_RB.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            m_JumpCount = 2;
            Debug.Log(m_JumpCount);


        }

        if (IsGrounded())
        {
            m_JumpCount = 0;
        }
    }

    private void FixedUpdate()
    {
        //Gets the direction where the character is moving
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + m_Cam.eulerAngles.y; //Calculates the angle where the character is facing

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; //Calculates the direction in which the player is moving

        if (direction.magnitude >= 0.1f) //If the player is moving
        {
            
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_RotationSpeed, m_TurnSmoothTime); // Function to smooth the angle movement
            transform.rotation = Quaternion.Euler(0f, angle, 0f); // Make the character actually rotate
            //Add force to the rigidbody to move the character
            m_RB.AddForce(moveDir.normalized * m_PlayerSpeed, ForceMode.Impulse);
        }
        else
        {

            //Stops the floor from being slippery
            Vector3 lateralVel = Vector3.ProjectOnPlane(m_RB.velocity, Vector3.up); // Creates a plane with the x and z
            if (lateralVel.magnitude > 0.1f)
            {
                // if when the player stops the vector in plane is more than 0.1, then add force to stop the player
                m_RB.AddForce(-(lateralVel.normalized * m_StopingForce * Time.fixedDeltaTime), ForceMode.Impulse);
                //Set the player run animation to false
                

            }
            else
            {
                //makes the player able to jump
                m_RB.velocity = m_RB.velocity.y * Vector3.up;
            }
        }

        //Velocity Cap. If the velocity is more than the maximum speed then limit that velocity
        if (m_RB.velocity.magnitude > m_MaxSpeed)
        {
            m_RB.velocity = m_RB.velocity.normalized * m_MaxSpeed;
        }


        //if (m_JumpCount == 0 && Input.GetButton("Jump")/* && m_JumpCounted == false*/)
        //{
        //    m_RB.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        //    //m_JumpCounted = true;
        //    m_JumpCount = 1;
        //    Debug.Log(m_JumpCount);

        //}

        //if (!IsGrounded() && m_JumpCount == 1 && Input.GetButton("Jump"))
        //{
        //    m_RB.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        //    m_JumpCount = 2;
        //    Debug.Log(m_JumpCount);


        //}

        //if (IsGrounded())
        //{
        //    m_JumpCount = 0;
        //}

    }

    public bool GetJumpingInput()
    {
        return Input.GetButton("Jump");
    }

    private bool IsGrounded()
    {
        m_IsGrounded = Physics.CapsuleCast(m_PlayerCollider.bounds.center, m_PlayerCollider.bounds.size, m_PlayerCollider.radius, Vector3.down, m_PlayerCollider.bounds.extents.y, m_Ground);
        return m_IsGrounded;
    }
}
