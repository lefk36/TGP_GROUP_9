using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTerrainCollision : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private LayerMask m_Ground;
    
    private void FixedUpdate()
    {
        Debug.DrawLine(transform.parent.position, transform.position, Color.black);
        ///Explanation of this script
        ///It creates a linecast from the cameraholder to the camera, and if the lineCast hits an object with "Ground" layer, 
        ///then the position of the camera is equal to the point of the raycast hit
        if(Physics.Linecast(transform.parent.position, transform.position, out hit, m_Ground))
        {
            Debug.DrawLine(transform.parent.position, (hit.point * 0.87f), Color.red);
            
            transform.position = (hit.point * 0.87f);
            
        }
    }
}
