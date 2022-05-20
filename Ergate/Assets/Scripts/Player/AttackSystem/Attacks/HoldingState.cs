using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingState : AttackState
{
    public override void StartAttack(Weapon caller)
    {
        base.StartAttack(caller);
    }
    public override void CancelAttack(MonoBehaviour caller)
    {
        base.CancelAttack(caller);
    }
    protected override IEnumerator AttackCoroutine()
    {
        completed = false;
        playerScript.lockMovement = true;
        playerScript.stickToAttack = true;
        if (animationTrigger != null)
        {
            playerAnimator.SetTrigger(animationTrigger);
        }
        while (!Input.GetButtonUp("BasicAttack"))
        {
            if (!Input.GetButton("BasicAttack"))
            {
                break;
            }
            yield return null;
        }
        playerScript.lockMovement = false;
        playerScript.stickToAttack = false;
        completed = true;
    }
}
