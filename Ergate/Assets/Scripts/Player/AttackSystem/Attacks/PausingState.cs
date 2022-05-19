using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingState : AttackState
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
        playerScript.lockAttackDirection = true;
        playerScript.lockMovement = true;
        playerScript.stickToAttack = true;
        if (animationTrigger != null)
        {
            playerAnimator.SetTrigger(animationTrigger);
        }
        yield return new WaitForSeconds(attackTime);
        playerScript.stickToAttack = false;
        playerScript.lockAttackDirection = false;
        playerScript.lockMovement = false;
        completed = true;
        yield return null;
    }
}
