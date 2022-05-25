using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTerrainCollision : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private LayerMask m_Ground;
    private Vector3 m_offset;

    private void Start()
    {
        m_offset = transform.localPosition; //calculate the offset vector at start
    }

    private void Update()
    {
        Vector3 originalCameraPosition = transform.parent.position + (transform.parent.rotation * m_offset); //calculate the pre-terrain collision position
        Debug.DrawLine(transform.parent.position, originalCameraPosition, Color.black);
        //Creates a linecast from the camera center to the pre-terrain collision position
        bool originalPositionHit = Physics.Linecast(transform.parent.position, originalCameraPosition, out hit, m_Ground);
        //If the linecast hits on something then the position of the camera is equal to the point where the linecast hits
        if (originalPositionHit)
        {
            float newOffsetDistance = ((hit.point - transform.parent.position) * 0.95f).magnitude;
            transform.localPosition = m_offset.normalized * newOffsetDistance;

        }
        //If not, then sets the localPosition to the offset camera position
        else
        {
            transform.localPosition = m_offset;
        }
    } 
}
