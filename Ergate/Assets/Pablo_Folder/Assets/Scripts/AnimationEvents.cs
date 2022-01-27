using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator m_PlayerAnimator; //Animator of the player
    public void KnockedDownAnimFinished(string check)
    {
        if (check.Equals("KnockedDownAnimationFinished"))
        {
            m_PlayerAnimator.SetBool("KnockedDown", false);
        }
    }
}
