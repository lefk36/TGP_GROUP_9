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
        playerScript.allowDash = false;
        yield return new WaitForSeconds(attackBeginningTime);
        if (animationTrigger != null)
        {
            playerAnimator.SetTrigger(animationTrigger);
        }
        Rigidbody rb = playerScript.gameObject.GetComponent<Rigidbody>();
        playerScript.lockFalling = true;
        playerScript.m_audioController.GetComponent<audioController>().play(soundName);
        attackInstance = Object.Instantiate(attackObject, attackParentObj);
        Vector3 newAttackDirection = attackParentObj.rotation * attackDirection;
        float distanceTravelled = 0;
        float newAttackRange = attackRange;
        if (toEnemy)
        {
            newAttackRange = FindNewRange();
        }
        float timeWhileNotMoving = 0;
        do
        {
            rb.velocity = newAttackDirection * speed;
            Vector3 lastPos = playerScript.transform.position;
            yield return null;
            Vector3 currentPosition = playerScript.transform.position;
            float nextStep = (lastPos - currentPosition).magnitude;
            distanceTravelled += nextStep;
            if (nextStep <= 0.01)
            {
                timeWhileNotMoving += Time.deltaTime;
            }
            else
            {
                timeWhileNotMoving = 0;
            }
            if (timeWhileNotMoving > 0.5f)
            {
                break;
            }
        } while (distanceTravelled < newAttackRange);
        playerScript.lockFalling = false;
        rb.velocity = rb.velocity / stoppingPower;
        yield return new WaitForSeconds(attackEndTime);
        playerScript.stickToAttack = false;
        playerScript.lockAttackDirection = false;
        playerScript.lockMovement = false;
        playerScript.allowDash = true;
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
