using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public Vector3 m_grapplePoint;
    public Vector3 m_currentLoc;
    private bool m_yUp;
    public float m_swingYAmount;
    public float m_swingZAmount;
    public float m_swingXAmount;
    private Rigidbody m_RB;
    private bool flip;
    private float m_radius;
    private GameObject m_Player;

    void Start()
    {
        m_currentLoc = gameObject.transform.position;
        Vector3 tempCurrentLoc = m_currentLoc - m_grapplePoint;


        m_radius = Vector3.Distance(m_currentLoc, m_grapplePoint);
        //Debug.Log("the radius is: " + m_radius);
        flip = false;

        m_RB = gameObject.GetComponent<Rigidbody>();

        m_yUp = true;


    }

    private void newIdea()
    {
        m_currentLoc = gameObject.transform.position;
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
                xzAngle = 90f;
                xzAngle *= Mathf.Deg2Rad;
                //m_increase = true;
                //Debug.LogError("setting angle to 90");

            }
            else if (tempCurrentLoc.y < 0f)
            {
                xzAngle = -90f;
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



        Vector3 tempDir = new Vector3(-tempCurrentLoc.x, tempCurrentLoc.y, -tempCurrentLoc.z);
        //Debug.LogError("m_yup is: " + m_yUp);
        if(!m_yUp)
        {
            if(flip)
            {
                tempDir.y = tempCurrentLoc.y;
                tempDir.x = -tempCurrentLoc.x;
                tempDir.z = -tempCurrentLoc.z;
            }
            else
            {
                tempDir.y = -tempCurrentLoc.y;
                tempDir.x = -tempCurrentLoc.x;
                tempDir.z = -tempCurrentLoc.z;
            }

            
        }
        else if(m_yUp)
        {
            if(flip)
            {
                tempDir.y = tempCurrentLoc.y;
                tempDir.x = -tempCurrentLoc.x;
                tempDir.z = -tempCurrentLoc.z;
            }
            else
            {
                tempDir.y = -tempCurrentLoc.y;
                tempDir.x = -tempCurrentLoc.x;
                tempDir.z = -tempCurrentLoc.z;
            }
            
        }
        tempCurrentLoc += m_grapplePoint;
        tempDir.Normalize();

        float oppX = -tempCurrentLoc.x;
        float oppZ = -tempCurrentLoc.z;
        float diffY = tempCurrentLoc.y - m_currentLoc.y;
        
        Vector3 direction;
        direction = (tempCurrentLoc - m_currentLoc);
        //direction = new Vector3(oppX, 0f, oppZ);
        //direction = (direction - m_currentLoc);
       
        Debug.Log("the direction is: " + direction);
        //m_RB.velocity.Set(0f,0f,0f);
        //m_RB.AddForce(tempDir, ForceMode.Force);
        //m_RB.position = tempCurrentLoc;
        m_RB.MovePosition(tempCurrentLoc);
       


    }
    private void FixedUpdate()
    {
        //m_RB.AddForce(transform.right * 0.1f, ForceMode.Force);
        newIdea();
        m_Player = FindObjectOfType<PlayerController>().gameObject;
        //m_Player.transform.position = gameObject.transform.position;
        
    }

}
