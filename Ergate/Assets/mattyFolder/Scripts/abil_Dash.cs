using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abil_Dash : MonoBehaviour
{
    private bool m_IsOnGround;
    private float m_DashCoolDownTimer = 0;
    public float m_DashCooolDown = 2f;

    // Update is called once per frame
    void Update()
    {
        m_IsOnGround = GetComponent<PlayerController>().isOnGround;
        

    }
}
