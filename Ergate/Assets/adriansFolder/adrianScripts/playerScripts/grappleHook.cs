using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappleHook : MonoBehaviour
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
        float yAxisAngle;
        float zAxisAngle;
        float xAxisAngle;

        float xzAngle = 0f;
        Vector3 xzPoint = new Vector3(tempCurrentLoc.x, m_grapplePoint.y, tempCurrentLoc.z);
        float xzDistance = Vector3.Distance(m_grapplePoint, xzPoint);

        m_radius = Vector3.Distance(m_grapplePoint, tempCurrentLoc);
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
       
        if (m_yUp)
        {
            xzAngle = xzAngle * Mathf.Rad2Deg;
            xzAngle += m_swingYAmount;
            
            xzAngle = xzAngle * Mathf.Deg2Rad;
            tempCurrentLoc.y = Mathf.Sin(xzAngle) * m_radius;
            xzPoint = new Vector3(tempCurrentLoc.x, m_grapplePoint.y, tempCurrentLoc.z);
            

        }
        else if (!m_yUp)
        {


            xzAngle = xzAngle * Mathf.Rad2Deg;
            
            xzAngle -= m_swingYAmount;
            xzAngle = xzAngle * Mathf.Deg2Rad;
            tempCurrentLoc.y = Mathf.Sin(xzAngle) * m_radius;
            xzPoint = new Vector3(tempCurrentLoc.x, m_grapplePoint.y, tempCurrentLoc.z);
            
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
            if(flip)
            {
                flip = false;
            }
            else if(!flip)
            {
                flip = true;
            }

            //Debug.LogWarning("z was: " + tempCurrentLoc.z);
            

           // Debug.LogWarning("has flipped");
            //Debug.LogWarning("z is: " + tempCurrentLoc.z);
            
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
            //Debug.LogError("going up in angle");
            

            if(flip)
            {
                if(m_swingYAmount > 0)
                {
                    yAngleForZ = -Mathf.Asin(tempCurrentLoc.x / xzDistance);
                    yAngleForX = -Mathf.Asin(tempCurrentLoc.z / xzDistance);
                    tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                    tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);
                }
                else if(m_swingYAmount < 0)
                {
                    yAngleForZ = Mathf.Asin(tempCurrentLoc.x / xzDistance);
                    yAngleForX = Mathf.Asin(tempCurrentLoc.z / xzDistance);
                    tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                    tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);
                }
                
                flip = false;

            }
            else if(!flip)
            {
                yAngleForZ = Mathf.Asin(tempCurrentLoc.x / xzDistance);
                yAngleForX = Mathf.Asin(tempCurrentLoc.z / xzDistance);
                tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);
                //Debug.Log("y angle for x is: " + yAngleForX);

            }
           

        }
        else if (!m_yUp)
        {
           // Debug.LogError("going down in angle");
            
            if (flip)
            {
                yAngleForZ = Mathf.Asin(tempCurrentLoc.x / xzDistance);
                yAngleForX = Mathf.Asin(tempCurrentLoc.z / xzDistance);
                tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);
                //Debug.Log("y angle for x is: " + yAngleForX);
                flip = false;

            }
            else if (!flip)
            {
                yAngleForZ = Mathf.Asin(tempCurrentLoc.x / xzDistance);
                yAngleForX = Mathf.Asin(tempCurrentLoc.z / xzDistance);
                tempCurrentLoc.x = (Mathf.Sin(yAngleForZ) * newXZDistance);
                tempCurrentLoc.z = (Mathf.Sin(yAngleForX) * newXZDistance);
                //Debug.Log("y angle for x is: " + yAngleForX);

            }

        }
        else
        {
            
            return;
        }



        //m_RB.AddForce(tempCurrentLoc, ForceMode.Impulse);
        tempCurrentLoc += m_grapplePoint;
        //tempCurrentLoc = (tempCurrentLoc - m_currentLoc).normalized;
        //tempCurrentLoc = -tempCurrentLoc;
        //tempCurrentLoc.Normalize();

        float oppX = -tempCurrentLoc.x;
        float oppZ = -tempCurrentLoc.z;
        float diffY = tempCurrentLoc.y - m_currentLoc.y;
        //Vector3 direction = new Vector3(oppX, 0, oppZ);
        Vector3 direction;
        direction = new Vector3(oppX, 0f, oppZ);
        direction = (direction - m_currentLoc);
        //direction = (direction - m_grapplePoint).normalized;
        Debug.Log("the direction is: " + direction);
        
        
        m_RB.MovePosition(new Vector3(m_currentLoc.x, diffY + m_currentLoc.y, m_currentLoc.z));
        m_RB.AddForce(direction, ForceMode.Force);
        //m_RB.velocity = direction;
        //m_RB.AddForce(direction, ForceMode.Force);

        //m_RB.AddRelativeForce(tempCurrentLoc, ForceMode.Force);

        //all of this above allows for the grapple hook angle to work


    }
    private void FixedUpdate()
    {
        newIdea();
    }

}
