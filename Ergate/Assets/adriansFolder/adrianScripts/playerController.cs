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


    private void Start()
    {
        m_RB = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
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

    private void controls()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            m_RB.AddForce(transform.forward * m_moveSpeed, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_RB.AddForce(-transform.forward * m_moveSpeed, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_RB.AddForce(transform.right * m_moveSpeed, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_RB.AddForce(-transform.right * m_moveSpeed, ForceMode.Impulse);
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        openDoor();
        controls();
        float yRot = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        float xRot = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        cameraRot -= yRot;
        cameraRot = Mathf.Clamp(cameraRot, -100f, 100f);



        m_camera.transform.localRotation = Quaternion.Euler(cameraRot, 0f, 0f);
        transform.Rotate(Vector3.up * xRot);
    }
}
