using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlamAttack : AttackState
{
    public override void StartAttack(Weapon caller)
    {
        base.StartAttack(caller);
    }
    public override void CancelAttack(MonoBehaviour caller)
    {
        if (playerScript != null)
        {
            if (playerScript.isOnGround)
            {
                attackInstance = Object.Instantiate(attackObject, attackParentObj);
                attackInstance.transform.parent = null;
            }
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
        playerScript.lockFalling = false;
        while (!playerScript.isOnGround)
        {
            rb.AddForce(attackDirection * speed, ForceMode.Force);
            yield return null;
        }
        playerScript.stickToAttack = false;
        playerScript.lockAttackDirection = false;
        playerScript.lockMovement = false;
        completed = true;
    }
}
