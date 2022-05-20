using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAirAttack : AttackState
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
        playerScript.lockFalling = true;
        Rigidbody rb = playerScript.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        if (animationTrigger != null)
        {
            playerAnimator.SetTrigger(animationTrigger);
        }
        yield return new WaitForSeconds(attackBeginningTime);
        rb.AddForce(0, 1 * attackRange, 0, ForceMode.Impulse);
        attackInstance = Object.Instantiate(attackObject, attackParentObj);
        yield return new WaitForSeconds(attackTime);
        playerScript.lockFalling = false;
        yield return new WaitForSeconds(attackEndTime);
        playerScript.stickToAttack = false;
        playerScript.lockAttackDirection = false;
        playerScript.lockMovement = false;
        completed = true;
    }
}
