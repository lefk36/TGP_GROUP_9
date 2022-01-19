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
        Vector3 originalCameraPosition = transform.parent.position + (transform.parent.localRotation * m_offset); //calculate the pre-terrain collision position
        Debug.DrawLine(transform.parent.position, originalCameraPosition, Color.black);
        ///Explanation of this script
        ///It creates a linecast from the cameraholder to the camera, and if the lineCast hits an object with "Ground" layer, 
        ///then the position of the camera is equal to the point of the raycast hit
        bool originalPositionHit = Physics.Linecast(transform.parent.position, originalCameraPosition, out hit, m_Ground);

        if (originalPositionHit)
        {
            transform.position = hit.point*0.95f;

        }
        else
        {
            transform.localPosition = m_offset;
        }
    } 
}
