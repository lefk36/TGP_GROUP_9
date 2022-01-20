using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAt : MonoBehaviour
{
    public Transform targetObj;

    // Update is called once per frame
    void Update()
    {
        if(targetObj == null)
        {
            Vector3 target = transform.position + (transform.parent.localRotation * Vector3.forward);
            transform.LookAt(target);
        }
        else
        {
            transform.LookAt(targetObj);
        }
        
    }
}
