using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingItem : MonoBehaviour
{
    public Camera m_cam;

    private void LateUpdate()
    {
        transform.forward = m_cam.transform.forward;
    }
}
