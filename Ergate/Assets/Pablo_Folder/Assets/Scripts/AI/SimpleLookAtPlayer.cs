using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAtPlayer : MonoBehaviour
{
    private Transform m_TargetObj;

    private void Start()
    {
        m_TargetObj = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.LookAt(m_TargetObj);
    }
}
