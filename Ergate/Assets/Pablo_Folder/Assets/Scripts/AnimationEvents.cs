using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator m_PlayerAnimator; //Animator of the player
    private GameObject m_Character;
    private GameObject m_Model;
    private void Start()
    {
        m_Character = GameObject.Find("Character");
        if (m_Character != null)
        {
            m_Model = GameObject.Find("Model");
            if (m_Model != null)
            {
                m_PlayerAnimator = m_Model.GetComponent<Animator>();

            }
        }
    }
    public void KnockedDownAnimFinished(string check)
    {
        if (check.Equals("KnockedDownAnimationFinished"))
        {
            m_PlayerAnimator.SetBool("KnockedDown", false);
            Debug.Log("Up");
        }
    }
}
