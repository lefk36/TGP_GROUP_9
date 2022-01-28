using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAt : MonoBehaviour
{
    public Transform targetObj;
    [SerializeField] private float m_SmoothRotation;

    private void FixedUpdate()
    {
        //If there is no traget to look at, then set the look at to the original point of the camera
        if(targetObj == null)
        {
            Vector3 lookTarget = transform.position + (transform.parent.localRotation * Vector3.forward);
            RotateToTargetSmoothly(lookTarget, m_SmoothRotation);
        }
        //If there is a target to look at then sets the look at to the targetObj transform;
        else
        {
            RotateToTargetSmoothly(targetObj.position, m_SmoothRotation);
        }
    }

    //Function to smooth the rotation between the target and the position of the camera
    private void RotateToTargetSmoothly(Vector3 target, float smoothness)
    {
        Vector3 direction = target - transform.position;
        Quaternion lookTargetRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookTargetRotation, smoothness * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
