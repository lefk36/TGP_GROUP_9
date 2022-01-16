using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public variables
    [SerializeField] private LayerMask m_Ground; //Ground layr mask
    [Min(0f)] public float acceleration;
    [Min(0f)] public float deceleration;
    [Min(0f)] public float targetSpeed;
    [Min(0f)] public float rotationTime;
    [Min(0f)] public float jumpForce;
    [Min(1.0f)] public float jumpingGravityScale;
    [Min(1.0f)] public float fallingMultiplier;

    public bool lockMovement; //Access this property from other scripts whenever they move the character instead
    public bool lockFalling;

    [HideInInspector] bool readyForAction = true; //Use this property in other scripts to check if the player is currently in the middle of another action (example: atttacking or knocked on the ground)
    [HideInInspector] bool isOnGround = true;

    //this object's components
    private new Rigidbody rigidbody;
    private CapsuleCollider movementCollider;
    private GravityScaler gravityScaleScript;

    //character object and its components
    private GameObject character;
    private  Collider[] hitboxes;

    //model object and its components
    private GameObject model;
    private Animator animator;

    //camera centre to find the direction its looking in
    private Transform cameraCentre;

    //maths variables
    private Vector3 movementDirection;
    float angularVelocity;

    //private bools
    bool jumping = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementCollider = GetComponent<CapsuleCollider>();
        gravityScaleScript = GetComponent<GravityScaler>();
        //load character information
        character = transform.Find("Character").gameObject;
        if (character != null)
        {
            model = character.transform.Find("Model").gameObject;
            if (model != null)
            {
                animator = model.GetComponent<Animator>();
                hitboxes = model.GetComponentsInChildren<Collider>();
            }
            else Debug.Log("No child object with the name 'Model' was found");
        }
        else Debug.Log("No child object with the name 'Character' was found");
        cameraCentre = transform.Find("Camera Centre");
        if (cameraCentre == null)
        {
            Debug.Log("No child object with the name 'Camera Centre' was found");
        }


    }
    void Update()
    {
        //update handles logic such as input. Physics calculations are done in fixed update

        //find if player is on ground. small modifications to the collider are made to make the result more accurate
        RaycastHit hit;
        isOnGround = Physics.SphereCast(movementCollider.bounds.center, movementCollider.radius-0.2f, Vector3.down, out hit, movementCollider.bounds.extents.y-0.2f, m_Ground);

        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")); //first the character's direction is defined by the inputs
        Quaternion cameraYRotation = Quaternion.Euler(0, cameraCentre.rotation.eulerAngles.y, 0);
        inputDirection = cameraYRotation * inputDirection; //transform the direction vector by camera centre's quaternion to make the direction relative to camera
        if (inputDirection.magnitude >= 0.1f && !lockMovement) //if user is pressing a movement button
        {
            RotateCharacterToDirection(inputDirection);
            movementDirection = inputDirection.normalized;
        }
        else
        {
            movementDirection = new Vector3(0, 0, 0);
        }

        if(Input.GetButtonDown("Jump") && !lockMovement && isOnGround)
        {
            jumping = true;
        }
    }
    private void FixedUpdate()
    {
        if (lockFalling)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            gravityScaleScript.gravityScale = 0;
        }
        else
        {
            gravityScaleScript.gravityScale = 1;
        }
        //reset gravity scalar when ground is hit
        if (isOnGround && !lockFalling)
        {
            gravityScaleScript.gravityScale = 1;
        }
        //if movement is locked, stop the player
        if (lockMovement)
        {
            movementDirection = Vector3.zero;
            rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, 0);
        }
        if (movementDirection.magnitude >= 0.1f && isOnGround) //if input detected
        {
            if (Mathf.Sign(movementDirection.x) != Mathf.Sign(rigidbody.velocity.x))
            {
                Vector3 opposite = new Vector3(-rigidbody.velocity.x, 0, 0);                                            //
                rigidbody.AddForce(opposite.normalized * deceleration, ForceMode.Acceleration);                         //
            }                                                                                                           //These if statements will determine if the input direction is different
            if (Mathf.Sign(movementDirection.z) != Mathf.Sign(rigidbody.velocity.z))                                    //to current velocity. If it is different, object will change direction faster
            {                                                                                                           //
                Vector3 opposite = new Vector3(0, 0, -rigidbody.velocity.z);                                            //
                rigidbody.AddForce(opposite.normalized * deceleration, ForceMode.Acceleration);                         //
            }
            if (rigidbody.velocity.magnitude < targetSpeed)
            {
                rigidbody.AddForce(movementDirection * acceleration, ForceMode.Acceleration); //this line moves the player          
            }
        }
        else if (movementDirection.magnitude < 0.1f && rigidbody.velocity.magnitude > 0.1f && isOnGround) //if no input but still moving then decelerate
        {
            rigidbody.AddForce(-rigidbody.velocity.normalized * deceleration, ForceMode.Acceleration);
        }

        //jumping
        if(jumping && isOnGround)
        {
            jumping = false;
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if(rigidbody.velocity.y > 0.2f) //when rising
        {
            gravityScaleScript.gravityScale = jumpingGravityScale;
        }
        if(rigidbody.velocity.y < 0) //when falling
        {
            gravityScaleScript.gravityScale = jumpingGravityScale*fallingMultiplier;
        }
        

    }
    void RotateCharacterToDirection(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; //Calculates the angle on which to rotate the character
        float angle = Mathf.SmoothDampAngle(character.transform.eulerAngles.y, targetAngle, ref angularVelocity, rotationTime); // Function to smooth the angle movement
        character.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    public void TakeDamage(Vector3 impactDirection, int damage, int poiseDamage)
    {
        impactDirection = impactDirection.normalized;
        // implementation for taking damage. Interrupt attacking, calculate if the hit has knocked the player down. If it did, calculate how far they fly, if at all.
    }
}
