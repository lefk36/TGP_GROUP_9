using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    [SerializeField] Timer m_timer;


    private void Start()
    {
        m_timer.SetTime(100).BeginTimer();
    }

}
