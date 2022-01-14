using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Vector3 m_LocalCameraOffSet; //Position of the camera from the player.
    [SerializeField] private Transform m_TrackedObjectTransform; //Object that the camera will follow
    [SerializeField] private Transform m_RotationPoint;
    [SerializeField] private float m_AutoCamSpeed; //Speed in which the camra follows the player
    [SerializeField] private float m_Sensitivity; //Sensitivity of the camera

    private void FixedUpdate()
    {
        //AutoCamera
        Vector3 toTarget = m_TrackedObjectTransform.position - transform.position; //Vector between the character and the camera

        float worldYRotInRad = transform.rotation.eulerAngles.y * Mathf.Deg2Rad; //The y rotation of the camera in radians

        //Current position of the camera
        Vector3 currentCameraPosition = transform.position - new Vector3(m_LocalCameraOffSet.z * Mathf.Sin(worldYRotInRad) + m_LocalCameraOffSet.x * Mathf.Cos(worldYRotInRad),
                                                                    m_LocalCameraOffSet.y,
                                                                    m_LocalCameraOffSet.z * Mathf.Cos(worldYRotInRad) - m_LocalCameraOffSet.x * Mathf.Sin(worldYRotInRad));
        //Position of the offSetCamera
        Vector3 cameraOffSetPosition = transform.position - currentCameraPosition;

        //Distance from the camera to the target
        float distToTarget = (m_TrackedObjectTransform.position - currentCameraPosition).magnitude;

        //Makes the camera follow the player
        transform.position = Vector3.MoveTowards(currentCameraPosition, m_TrackedObjectTransform.position, distToTarget * m_AutoCamSpeed * Time.fixedDeltaTime) + cameraOffSetPosition;

        //Intent Camera
        if(Input.GetAxis("Mouse X") > 0f)
        {
            m_RotationPoint.rotation = m_RotationPoint.rotation * Quaternion.Euler(0f, Input.GetAxis("Mouse X") * 100 * m_Sensitivity * Time.fixedDeltaTime, 0f);
        }
        else if(Input.GetAxis("Mouse X") < 0f)
        {
            m_RotationPoint.rotation = m_RotationPoint.rotation * Quaternion.Euler(0f, Input.GetAxis("Mouse X") * 100 * m_Sensitivity * Time.fixedDeltaTime, 0f);
        }
        else if(Input.GetAxis("ControllerHorizontal") > 0)
        {
            m_RotationPoint.rotation = m_RotationPoint.rotation * Quaternion.Euler(0f, Input.GetAxis("ControllerHorizontal") * 100 * m_Sensitivity * Time.fixedDeltaTime, 0f);
        }
        else if (Input.GetAxis("ControllerHorizontal") < 0)
        {
            m_RotationPoint.rotation = m_RotationPoint.rotation * Quaternion.Euler(0f, Input.GetAxis("ControllerHorizontal") * 100 * m_Sensitivity * Time.fixedDeltaTime, 0f);
        }
    }


}
