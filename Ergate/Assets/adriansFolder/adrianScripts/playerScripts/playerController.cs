using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private int LayerMask = 1 << 6;
    public int m_moveSpeed = 5;
    public Camera m_camera;
    private GameObject m_doorToOpen;
    private Rigidbody m_RB;
    public float sensitivity = 5f;
    private float cameraRot = 0f;
    public GameObject m_projectile;
    public LayerMask m_slippyGround;
    public float health;

    private void Start()
    {
        m_RB = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        Debug.Log("current health is: " + health);
    }
    private void openDoor()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {


            RaycastHit hit;
            if (Physics.Raycast(transform.position, m_camera.transform.forward, out hit, Mathf.Infinity, LayerMask))
            {
                m_doorToOpen = hit.collider.gameObject;
                m_doorToOpen.GetComponent<gateScript>().openGate();
            }
        }

    }
    private void groundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f, m_slippyGround))
        {

           
            //m_RB.AddForce(-transform.up * m_moveSpeed, ForceMode.Force);
            m_RB.velocity.Scale(transform.forward * 5f);
            Debug.Log("hit slippery surface");
        }
    }

    private void controls()
    {
        if(Input.GetKey(KeyCode.W))
        {
            m_RB.AddForce(transform.forward * m_moveSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_RB.AddForce(-transform.forward * m_moveSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_RB.AddForce(transform.right * m_moveSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_RB.AddForce(-transform.right * m_moveSpeed, ForceMode.Force);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_RB.AddForce(transform.up * 10f, ForceMode.Impulse);
        }
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(m_projectile, transform.position + (transform.forward * 1.5f), m_camera.transform.rotation);
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        openDoor();
        controls();
        groundCheck();
        float yRot = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        float xRot = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        cameraRot -= yRot;
        cameraRot = Mathf.Clamp(cameraRot, -100f, 100f);



        m_camera.transform.localRotation = Quaternion.Euler(cameraRot, 0f, 0f);
        transform.Rotate(Vector3.up * xRot);
    }
}
