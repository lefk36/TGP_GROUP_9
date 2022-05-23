using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    [SerializeField] Timer m_timer;

    [SerializeField] int countTime;

    private void Start()
    {
        m_timer.SetTime(countTime).BeginTimer();
    }

   

}
