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
        if (attackInstance != null)
        {
            GameObject.Destroy(attackInstance);
        }
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
        yield return new WaitForSeconds(attackBeginningTime);
        playerScript.m_audioController.GetComponent<audioController>().play(soundName);
        attackInstance = Object.Instantiate(attackObject, attackParentObj);
        attackInstance.transform.parent = null;
        yield return new WaitForSeconds(attackTime);
        yield return new WaitForSeconds(attackEndTime);
        playerScript.stickToAttack = false;
        playerScript.lockAttackDirection = false;
        playerScript.lockMovement = false;
        completed = true;
    }
}
