using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousHoldingAttack : AttackState
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
        yield return new WaitForSeconds(attackBeginningTime);
        if (animationTrigger != null)
        {
            playerAnimator.SetTrigger(animationTrigger);
        }
        GameObject attackInstance = Object.Instantiate(attackObject, attackParentObj);
        while (!Input.GetButtonUp("AlternativeAttack"))
        {
            Debug.Log("Shredding..");
            if (!Input.GetButton("AlternativeAttack"))
            {
                break;
            }
            yield return null;
        }
        playerScript.lockAttackDirection = false;
        playerScript.lockMovement = false;
        playerScript.stickToAttack = false;
        completed = true;
        yield return null;
    }
}
