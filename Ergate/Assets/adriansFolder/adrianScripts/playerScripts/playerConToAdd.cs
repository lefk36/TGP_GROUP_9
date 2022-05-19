using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerConToAdd : MonoBehaviour
{
    //layermask for interaction
    public LayerMask m_checkpointMask;
    public LayerMask m_doorMask;

    private GameObject m_doorToOpen;
    public GameObject m_player;
    public Transform camera;

    private PlayerController m_PC;
    private GravityScaler m_GS;
    public Vector3 m_grapplePoint;
    public Vector3 m_currentLoc;
    private bool m_yUp;
    public float m_swingYAmount;
    public float m_swingZAmount;
    public float m_swingXAmount;
    private Rigidbody m_RB;
    private Rigidbody m_testRB;
    private bool flip;
    private float m_radius;

    private bool m_isSwinging;

    public GameObject m_testSwing;


    private void Start()
    {
        //m_RB = gameObject.GetComponent<Rigidbody>();
        m_RB = gameObject.GetComponent<Rigidbody>();
        flip = false;
        m_yUp = true;
        m_isSwinging = false;
        m_PC = gameObject.GetComponent<PlayerController>();
        m_GS = gameObject.GetComponent<GravityScaler>();
        m_testRB = m_testSwing.GetComponent<Rigidbody>();
    }

    private void interact()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.SphereCast(gameObject.transform.position, 2f, camera.forward, out hit, 4f, m_checkpointMask))
            {
                
                Debug.Log("did hit the checkpoint");
                hit.transform.gameObject.GetComponent<playerCheckpoint>().setSpawnLocation(m_player);
                
            }
            else
            {
                Debug.Log("did not hit the checkpoint");
            }

            if (Physics.Raycast(transform.position, camera.forward, out hit, Mathf.Infinity, m_doorMask))
            {
                m_doorToOpen = hit.collider.gameObject;
                m_doorToOpen.GetComponent<gateScript>().openGate();
            }
            
        }

        if(Input.GetKeyDown(KeyCode.E))
        {

            if(Physics.Raycast(transform.position,camera.forward,out hit, 10f))
            {
                m_grapplePoint = hit.point;
                Debug.LogError("the world point is: " + hit.point);
                m_isSwinging = true;
                //gameObject.GetComponent<Rigidbody>().isKinematic = true;
                m_PC.m_isSwinging = m_isSwinging;
                m_GS.m_isSwinging = m_isSwinging;
                m_testSwing.transform.position = gameObject.transform.position;
                m_RB.velocity.Set(0f, 0f, 0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.GetComponent<playerStats>().takeDamage(10f);

        }
    }

    private void Swinging()
    {

        m_currentLoc = m_testSwing.transform.position;
        m_RB.isKinematic = false;
        Vector3 tempCurrentLoc = m_currentLoc - m_grapplePoint;
        Vector3 tempGrapplePoint = m_grapplePoint - m_grapplePoint;
        float yAxisAngle;
        float zAxisAngle;
        float xAxisAngle;

        float xzAngle = 0f;
        Vector3 xzPoint = new Vector3(tempCurrentLoc.x, tempGrapplePoint.y, tempCurrentLoc.z);
        float xzDistance = Vector3.Distance(tempGrapplePoint, xzPoint);

        m_radius = Vector3.Distance(tempGrapplePoint, tempCurrentLoc);
        if (xzDistance == 0f)
        {
            if (tempCurrentLoc.y >= 0f)
            {
                xzAngle = 91f;
                xzAngle *= Mathf.Deg2Rad;
                //m_increase = true;
                //Debug.LogError("setting angle to 90");

            }
            else if (tempCurrentLoc.y < 0f)
            {
                xzAngle = -91f;
                //m_increase = false;
                xzAngle *= Mathf.Deg2Rad;
                //Debug.LogError("setting angle to minus 90");
            }


        }
        else
        {
            xzAngle = Mathf.Asin(tempCurrentLoc.y / m_radius);
        }
        xzAngle = Mathf.Asin(tempCurrentLoc.y / m_radius);
        if (m_yUp)
        {
            xzAngle = xzAngle * Mathf.Rad2Deg;
            xzAngle += m_swingYAmount;

            xzAngle = xzAngle * Mathf.Deg2Rad;
            tempCurrentLoc.y = Mathf.Sin(xzAngle) * m_radius;
            xzPoint = new Vector3(tempCurrentLoc.x, tempGrapplePoint.y, tempCurrentLoc.z);


        }
        else if (!m_yUp)
        {


            xzAngle = xzAngle * Mathf.Rad2Deg;

            xzAngle -= m_swingYAmount;
            xzAngle = xzAngle * Mathf.Deg2Rad;
            tempCurrentLoc.y = Mathf.Sin(xzAngle) * m_radius;
            xzPoint = new Vector3(tempCurrentLoc.x, tempGrapplePoint.y, tempCurrentLoc.z);

        }

        xzAngle *= Mathf.Rad2Deg;

        if (xzAngle >= 90f)
        {
            if (m_swingYAmount > 0)
            {
                m_yUp = false;
            }
            else if (m_swingYAmount < 0)
            {
                m_yUp = true;
            }
            if (flip)
            {
                flip = false;
            }
            else if (!flip)
            {
                flip = true;
            }



        }
        else if (xzAngle <= -90f)
        {


            if (m_swingYAmount > 0)
            {
                m_yUp = true;
            }
            else if (m_swingYAmount < 0)
            {
                m_yUp = false;
            }

        }

        xzAngle *= Mathf.Deg2Rad;


        float newXZDistance = Mathf.Cos(xzAngle) * m_radius;

        float yAngleForZ;
        float yAngleForX;


        if (m_yUp)
        {



            if (flip)
            {
                if (m_swingYAmount > 0)
                {
                    yAngleForZ = Mathf.Asin(tempCurrentLoc.x / xzDistance);
                    yAngleForX = Mathf.Asin(tempCurrentLoc.z / xzDistance);
                    tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                    tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);
                }
                else if (m_swingYAmount < 0)
                {
                    yAngleForZ = Mathf.Asin(tempCurrentLoc.x / xzDistance);
                    yAngleForX = Mathf.Asin(tempCurrentLoc.z / xzDistance);
                    tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                    tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);
                }

                flip = false;

            }
            else if (!flip)
            {
                yAngleForZ = Mathf.Asin(tempCurrentLoc.x / xzDistance);
                yAngleForX = Mathf.Asin(tempCurrentLoc.z / xzDistance);
                tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);


            }


        }
        else if (!m_yUp)
        {


            if (flip)
            {
                yAngleForZ = Mathf.Asin(tempCurrentLoc.x / xzDistance);
                yAngleForX = Mathf.Asin(tempCurrentLoc.z / xzDistance);
                tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);

                flip = false;

            }
            else if (!flip)
            {
                yAngleForZ = Mathf.Asin(tempCurrentLoc.x / xzDistance);
                yAngleForX = Mathf.Asin(tempCurrentLoc.z / xzDistance);
                tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);


            }

        }
        else
        {

            return;
        }




        tempCurrentLoc += m_grapplePoint;


        float oppX = -tempCurrentLoc.x;
        float oppZ = -tempCurrentLoc.z;
        float diffY = tempCurrentLoc.y - m_currentLoc.y;

        Vector3 direction;
        direction = new Vector3(oppX, 0f, oppZ);
        direction = (direction - m_currentLoc);

        Debug.Log("the direction is: " + direction);

        m_testRB.MovePosition(tempCurrentLoc);
        //m_RB.position = tempCurrentLoc;
    }

    
    public void stopSwinging()
    {
        m_isSwinging = false;
        m_RB.isKinematic = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    private void Update()
    {
        interact();
        if(m_isSwinging)
        {
            
            Swinging();
            gameObject.transform.position = m_testSwing.transform.position;
        }
    }

}
