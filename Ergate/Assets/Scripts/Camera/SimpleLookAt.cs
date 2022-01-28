using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAt : MonoBehaviour
{
    public Transform targetObj;

    void Update()
    {
        //If there is no traget to look at, then set the look at to the original point of the camera
        if(targetObj == null)
        {
            Vector3 target = transform.position + (transform.parent.localRotation * Vector3.forward);
            transform.LookAt(target);
        }
        //If there is a target to look at then sets the look at to the targetObj transform;
        else
        {
            transform.LookAt(targetObj);
        }
    }
}
