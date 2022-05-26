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
        if(attackInstance != null)
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
        yield return new WaitForSeconds(attackBeginningTime);
        if (animationTrigger != null)
        {
            playerAnimator.SetTrigger(animationTrigger);
        }
        attackInstance = Object.Instantiate(attackObject, attackParentObj);
        attackInstance.transform.GetChild(0).GetComponent<DealDamageTick>().SetAudio(soundName, playerScript.m_audioController.GetComponent<audioController>());
        while (!Input.GetButtonUp("AlternativeAttack"))
        {
            if (!Input.GetButton("AlternativeAttack"))
            {
                break;
            }
            yield return null;
        }
        GameObject.Destroy(attackInstance);
        yield return new WaitForSeconds(attackEndTime);
        playerScript.lockAttackDirection = false;
        playerScript.lockMovement = false;
        playerScript.stickToAttack = false;
        completed = true;
        yield return null;
    }
}
