using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStandingAttack : AttackState
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
        Object.Instantiate(attackObject, attackParentObj);
        yield return new WaitForSeconds(attackTime);
        playerScript.stickToAttack = false;
        playerScript.lockAttackDirection = false;
        playerScript.lockMovement = false;
        completed = true;
        yield return null;
    }
}
