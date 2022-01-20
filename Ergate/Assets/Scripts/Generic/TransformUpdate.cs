using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUpdate : MonoBehaviour
{
    public Vector3 myPosition;
    
    void Update()
    {
        transform.position = myPosition;
    }
}
