using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public variables
    [SerializeField] private LayerMask m_Ground; //Ground layr mask
    [Header("Running")]
    [Min(0f)] public float acceleration;
    [Min(0f)] public float deceleration;
    [Min(0f)] public float targetSpeed;
    [Min(0f)] public float rotationTime;
    [Header("Jumping")]
    [Min(0f)] public float jumpForce;
    [Min(0f)] public float doubleJumpForwardForce;
    [Min(1.0f)] public float jumpingGravityScale;
    [Min(1.0f)] public float fallingMultiplier;
    [SerializeField] private bool doubleJumpUnlocked;

    [Header("Logic")]
    public bool lockMovement; //Access these properties from other scripts whenever they move them instead
    public bool lockFalling;
    public bool lockAttackDirection;
    private bool m_VelocityStopped;

    [HideInInspector] public bool readyForAction = true; //Use this property in other scripts to check if the player is currently in the middle of another action (example: atttacking or knocked on the ground)
    [HideInInspector] public bool isOnGround = true;
    public bool cameraLockedToTarget; //edit this in camera script to determine whether

    //this object's components
    [HideInInspector] public new Rigidbody rigidbody;
    private CapsuleCollider movementCollider;
    [HideInInspector] public GravityScaler gravityScaleScript;
    public PlayerPoiseAndHealth m_PlayerStats;

    //character object and its components
    private GameObject character;
    private  Collider[] hitboxes;


    //model object and its components
    [HideInInspector] public GameObject model;
    [HideInInspector] public Animator animator;

    //camera objects to use for input calculations
    [HideInInspector] public GameObject cameraCentre;
    [HideInInspector] public new GameObject camera;
    public Camera mainCam;

    //Attack direction object that rotates with input or uses the camera to rotate.
    private Transform attackDirectionObject;

    //maths variables
    private Vector3 movementDirection;
    private float angularVelocity;
    private float attackDirectionAngularVelocity;

    //private bools
    bool jumping = false;
    bool doubleJumped = false;

    //variables for audio
    private GameObject m_audioController;
    private bool m_runAudio = false;


    private float m_MaxRunAnimSpeed = 7f;

    //variable to control dash length
    [Header("Dash")]
    public float m_dashSpeed;
    public float dashTime;
    public float dashStoppingPower;
    [HideInInspector] public bool m_hasDashed = false;
    private bool coroutineRunning = false;
    public float dashCooldown;

    //other scripts
    EnemiesCameraLock lockScript;
    [HideInInspector] public bool stickToAttack = false;

    private Vector3 m_InputDirection;

    [SerializeField] private AttackInput m_AttackInput;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementCollider = GetComponent<CapsuleCollider>();
        gravityScaleScript = GetComponent<GravityScaler>();
        m_audioController = FindObjectOfType<audioController>().gameObject;
        lockScript = transform.Find("Camera Centre").GetChild(0).GetComponent<EnemiesCameraLock>();
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

        cameraCentre = transform.Find("Camera Centre").gameObject;
        if (cameraCentre != null)
        {
            camera = cameraCentre.transform.Find("Main Camera").gameObject;
            if(camera == null)
            {
                Debug.Log("No child object with the name 'Main Camera' was found");
            }
            else
            {
                mainCam = camera.GetComponent<Camera>();
            }
        }
        else Debug.Log("No child object with the name 'Camera Centre' was found");

        attackDirectionObject = transform.Find("Attack Direction");
        if (attackDirectionObject == null)
        {
            Debug.Log("No child object with the name 'Attack Direction' was found");
        }


    }
    void Update()
    {
        //update handles logic. Actual Physics calculations are done in fixed update

        //find if player is on ground. small modifications to the collider are made to make the result more accurate
        RaycastHit hit;
        isOnGround = Physics.SphereCast(movementCollider.bounds.center, movementCollider.radius-0.2f, Vector3.down, out hit, movementCollider.bounds.extents.y-0.2f, m_Ground);

        m_InputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")); //first the character's direction is defined by the inputs
        Quaternion cameraYRotation = Quaternion.Euler(0, cameraCentre.transform.rotation.eulerAngles.y, 0);
        m_InputDirection = cameraYRotation * m_InputDirection; //transform the direction vector by camera centre's quaternion to make the direction relative to camera

        //if(pauseMenu.active == false)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
        //else
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}

        if (cameraLockedToTarget && !lockAttackDirection)
        {
            if(lockScript.m_TargetableEnemies[lockScript.m_TargetableEnemyIndex] != null)
            {
                Vector3 dirToEnemy = lockScript.m_TargetableEnemies[lockScript.m_TargetableEnemyIndex].transform.position - transform.position;
                RotateObjectToDirectionInstant(dirToEnemy, attackDirectionObject);
            }
            
        }
        else if (!cameraLockedToTarget && !lockAttackDirection)
        {
            RotateObjectToDirectionInstant(camera.transform.forward, attackDirectionObject);
        }
        if (m_InputDirection.magnitude >= 0.1f && !lockMovement) //if user is pressing a movement button
        {
            RotateObjectToDirection(m_InputDirection, character.transform, rotationTime, ref angularVelocity);
            movementDirection = m_InputDirection.normalized;
        }
        else
        {
            movementDirection = new Vector3(0, 0, 0);
        }
        if (stickToAttack)
        {
            RotateToAttackDirection();
        }

        dashControl();

        if (isOnGround)
        {
            doubleJumped = false;
        }
        if(Input.GetButtonDown("Jump") && !lockMovement && (isOnGround || (!doubleJumped && doubleJumpUnlocked)))
        {
            
            if((!doubleJumped && doubleJumpUnlocked)&& !isOnGround)
            {
                animator.SetTrigger("DoubleJump");
                doubleJumped = true;
            }
            else if(isOnGround)
            {
                animator.SetTrigger("Jump");
            }

            jumping = true;
            
        }
        if((Mathf.Abs(rigidbody.velocity.x) > 0.1f && Mathf.Abs(rigidbody.velocity.z) > 0.1f && m_InputDirection.magnitude > 0.2f) && isOnGround && !doubleJumped && !lockMovement)
        {
            animator.SetBool("IsRunning", true);
            
            if (!m_runAudio)
            {
                m_runAudio = true;
                m_audioController.GetComponent<audioController>().play("playerRunning");

            }

        }
        else 
        {
            
            animator.SetBool("IsRunning", false);
            if (m_runAudio)
            {
                m_audioController.GetComponent<audioController>().pauseClip("playerRunning");
                m_runAudio = false;
            }

        }

        //if(Input.GetButtonDown("Pause"))
        //{
        //    //Cursor.lockState = CursorLockMode.None;
        //    pauseMenu.SetActive(true);
        //    Time.timeScale = 0f;
        //}

        if((rigidbody.velocity.y < 0.1f || doubleJumped) && !isOnGround)
        {
            animator.SetTrigger("Falling");
        }
        else
        {
            animator.SetTrigger("BackToIdle");
        }

        animator.SetFloat("RunSpeed", rigidbody.velocity.magnitude / m_MaxRunAnimSpeed);
    }
    private void FixedUpdate()
    {
        
        if (lockFalling)
        {
            if(rigidbody.velocity.y < 0)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            }
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

        if(m_hasDashed && !coroutineRunning)
        {
            StartCoroutine(dash(m_InputDirection));
        }

        //if movement is locked, stop the player
        if (lockMovement)
        {
            if(!m_VelocityStopped)
            {
                movementDirection = Vector3.zero;
                rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
                m_VelocityStopped = true;
            }
           
        }
        else
        {
            m_VelocityStopped = false;
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
        if(jumping && !isOnGround && doubleJumpUnlocked)
        {
            jumping = false;
            Vector3 characterForward = character.transform.forward.normalized;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce((Vector3.up * jumpForce) + (characterForward * doubleJumpForwardForce), ForceMode.Impulse);
        }
        if(rigidbody.velocity.y > 0.2f && !lockFalling) //when rising
        {
            gravityScaleScript.gravityScale = jumpingGravityScale;
        }
        if(rigidbody.velocity.y < 0 && !lockFalling) //when falling
        {

            gravityScaleScript.gravityScale = jumpingGravityScale*fallingMultiplier;
        }
        

    }
    void RotateObjectToDirection(Vector3 direction, Transform target, float timeToRotate, ref float outVelocity)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; //Calculates the angle on which to rotate the character
        float angle = Mathf.SmoothDampAngle(target.eulerAngles.y, targetAngle, ref outVelocity, timeToRotate); // Function to smooth the angle movement
        target.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    void RotateObjectToDirection(float targetAngle, Transform target, float timeToRotate, ref float outVelocity)
    {
        float angle = Mathf.SmoothDampAngle(target.eulerAngles.y, targetAngle, ref outVelocity, timeToRotate); // Function to smooth the angle movement
        target.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    void RotateObjectToDirectionInstant(Vector3 direction, Transform target)
    {
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; //Calculates the angle on which to rotate the character
        target.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    void RotateObjectToDirectionInstant(float targetAngle, Transform target)
    {
        target.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }
    void RotateToAttackDirection()
    {
        float targetAngle = attackDirectionObject.localRotation.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(character.transform.localRotation.eulerAngles.y, targetAngle, ref angularVelocity, rotationTime);
        character.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
    }
    public void TakeDamage(Vector3 impactDirection, int damage, int poiseDamage)
    {
        impactDirection = impactDirection.normalized;
        // implementation for taking damage. Interrupt attacking, calculate if the hit has knocked the player down. If it did, calculate how far they fly, if at all.
        //Perhaps this will be edited to make a damage script with similar functionality between the player and the enemies.
    }

    private void dashControl()
    {
        if(!m_PlayerStats.m_IsDead)
        {
            if (m_hasDashed)
            {
                animator.SetBool("IsDashing", true);
            }
            else
            {
                animator.SetBool("IsDashing", false);
            }

            if (Input.GetButtonDown("dash") && !m_hasDashed && !coroutineRunning)
            {

                m_hasDashed = true;
                m_AttackInput.CancelAttacks();
            }
        }
        
    }

    private IEnumerator dash(Vector3 inputDirection)
    {
        
        coroutineRunning = true;
        lockMovement = true;
        lockFalling = true;
        Vector3 dashDirection = inputDirection;
        //if no input detected
        if(inputDirection.magnitude < 0.2f)
        {
            dashDirection = Quaternion.Euler(0, cameraCentre.transform.rotation.eulerAngles.y, 0) * new Vector3(0, 0, 1);
        }
        float elapsedTime = 0;
        while(elapsedTime < dashTime)
        {
            elapsedTime += Time.deltaTime;
            rigidbody.velocity = dashDirection * m_dashSpeed;
            RotateObjectToDirection(dashDirection, character.transform, rotationTime, ref angularVelocity);
            yield return null;
        }
        lockFalling = false;
        lockMovement = false;
        rigidbody.velocity = rigidbody.velocity / dashStoppingPower;
        m_hasDashed = false;
        yield return new WaitForSeconds(dashCooldown);
        coroutineRunning = false;
    }


}
