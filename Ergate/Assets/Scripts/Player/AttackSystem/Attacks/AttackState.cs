using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState
{
    [HideInInspector] public bool completed = false;
    protected PlayerController playerScript;
    protected Animator playerAnimator;
    protected Transform attackParentObj;
    protected Vector3 targetPos;
    protected GameObject attackObject;
    Coroutine thisAttackRoutine;
    protected float attackBeginningTime;
    protected float attackTime;
    protected float attackRange;
    protected string animationTrigger;
    protected Vector3 attackDirection;
    protected bool toEnemy;
    protected float speed;
    public void SetAttackObject(GameObject p_attackObject)
    {
        attackObject = p_attackObject;
    }
    public void SetAttackDataVariables(float p_beginningTime, float p_attackTime, float p_range, string animationTrigger, Vector3 p_attackDirection, bool p_toEnemy, float p_speed)
    {
        attackBeginningTime = p_beginningTime;
        attackTime = p_attackTime;
        attackRange = p_range;
        toEnemy = p_toEnemy;
        attackDirection = p_attackDirection;
        speed = p_speed;
    }
    public virtual void SetVariables(PlayerController p_playerScript, Transform p_attackParentObj, Vector3 p_targetPos)
    {
        playerScript = p_playerScript;
        playerScript.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
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
