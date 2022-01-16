using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 Use this script when an object rotates differently to parent object. Update the myRotation variable from outside this script
 */
public class RotationUpdate : MonoBehaviour
{
    public Vector3 myRotation;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(myRotation);
    }
}
