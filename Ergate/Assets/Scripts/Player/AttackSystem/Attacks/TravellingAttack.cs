using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingAttack : AttackState
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
        Rigidbody rb = playerScript.gameObject.GetComponent<Rigidbody>();
        playerScript.lockFalling = true;
        attackInstance = Object.Instantiate(attackObject, attackParentObj);
        Vector3 newAttackDirection = attackParentObj.rotation * attackDirection;
        float distanceTravelled = 0;
        Vector3 startPosition = playerScript.transform.position;
        float newAttackRange = attackRange;
        if (toEnemy)
        {
            newAttackRange = FindNewRange();
        }
        while(distanceTravelled < newAttackRange)
        {
            rb.velocity = newAttackDirection * speed;
            yield return null;
            Vector3 currentPosition = playerScript.transform.position;
            distanceTravelled = (startPosition - currentPosition).magnitude;
        }
        playerScript.lockFalling = false;
        rb.velocity = rb.velocity / stoppingPower;
        yield return new WaitForSeconds(attackEndTime);
        playerScript.stickToAttack = false;
        playerScript.lockAttackDirection = false;
        playerScript.lockMovement = false;
        GameObject.Destroy(attackInstance);
        completed = true;
    }
    protected float FindNewRange()
    {
        //if the enemy is past the range, return attack range. if the enemy is close, return 0. if the enemy is closer than range, return distance.
        float distanceToEnemy = (playerScript.transform.position - targetPos).magnitude;
        if(distanceToEnemy < 1)
        {
            distanceToEnemy = 0;
        }
        else
        {
            distanceToEnemy -= 1;
        }
        if(distanceToEnemy > 0.1f)
        {
            if(distanceToEnemy < attackRange)
            {
                return distanceToEnemy;
            }
            else
            {
                return attackRange;
            }
        }
        else
        {
            return 0;
        }
    }
}
