using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingStateAlternative : AttackState
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
        playerScript.lockFalling = true;
        Rigidbody rb = playerScript.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        if (animationTrigger != null)
        {
            playerAnimator.SetTrigger(animationTrigger);
        }
        while (!Input.GetButtonUp("AlternativeAttack"))
        {
            if (!Input.GetButton("AlternativeAttack"))
            {
                break;
            }
            yield return null;
        }
        playerScript.lockMovement = false;
        playerScript.stickToAttack = false;
        playerScript.lockFalling = false;
        completed = true;
    }
}
