using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTerrainCollision : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private LayerMask m_Ground;
    [SerializeField] private Transform m_Target;
    private Vector3 m_offset;

    private void Start()
    {
        m_offset = transform.position - transform.parent.position;
        Debug.Log("offset: " + m_offset);
    }

    private void Update()
    {
        Vector3 originalCameraPosition = transform.parent.position + m_offset;
        originalCameraPosition =  transform.parent.rotation * originalCameraPosition;
        Debug.DrawLine(m_Target.position, transform.position, Color.black);
        ///Explanation of this script
        ///It creates a linecast from the cameraholder to the camera, and if the lineCast hits an object with "Ground" layer, 
        ///then the position of the camera is equal to the point of the raycast hit
        bool positionHit = Physics.Linecast(m_Target.position, transform.position, out hit, m_Ground);
        bool originalPositionHit = Physics.Linecast(m_Target.position, originalCameraPosition, out hit, m_Ground);
        

        if (positionHit && originalPositionHit)
        {
            Debug.Log("Terrain collision");
            Vector3 hitVector = transform.position - hit.point;
            Debug.DrawLine(m_Target.position, (hit.point * 0.87f), Color.red);
            //Debug.Log(hit.distance);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + hitVector.magnitude + 0.1f);
            //transform.position = transform.position - hitVector;

        }
        
        if (originalPositionHit && !positionHit)
        {
            //transform.position = (hit.point * 0.95f);
        }

        Debug.Log("original position hit: " + originalPositionHit);
        Debug.Log("current position hit: " + positionHit);
        if (!positionHit && !originalPositionHit)
        {
            Debug.Log("Camera Position Reset");
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -4.85f);
        }
    } 
}
