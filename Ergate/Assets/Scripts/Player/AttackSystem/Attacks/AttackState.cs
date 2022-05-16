using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState
{
    [HideInInspector] public bool completed = false;
    protected PlayerController playerScript;
    protected Transform attackParentObj;
    protected Vector3 targetPos;
    protected GameObject attackObject;
    Coroutine thisAttackRoutine;
    protected float attackBeginningTime;
    protected float attackTime;
    protected float attackRange;
    public void SetAttackObject(GameObject p_attackObject)
    {
        attackObject = p_attackObject;
    }
    public void SetFloats(float p_beginningTime, float p_attackTime, float p_range)
    {
        attackBeginningTime = p_beginningTime;
        attackTime = p_attackTime;
        attackRange = p_range;
    }
    public virtual void SetVariables(PlayerController p_playerScript, Transform p_attackParentObj, Vector3 p_targetPos)
    {
        playerScript = p_playerScript;
        attackParentObj = p_attackParentObj;
        targetPos = p_targetPos;
    }
    public virtual void StartAttack(Weapon caller)
    {
        //start the coroutine on the caller MonoBehaviour
        thisAttackRoutine = caller.StartCoroutine(AttackCoroutine());
    }
    public virtual void CancelAttack(MonoBehaviour caller)
    {
        //stop the coroutine reference on the caller;
        completed = false;
        if (thisAttackRoutine != null)
        {
            caller.StopCoroutine(thisAttackRoutine);
        }
    }
    protected virtual IEnumerator AttackCoroutine()
    {
        completed = true;
        yield return null;
    }
}
