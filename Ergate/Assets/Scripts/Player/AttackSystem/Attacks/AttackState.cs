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
    protected float attackEndTime;
    protected float attackRange;
    protected string animationTrigger;
    protected Vector3 attackDirection;
    protected bool toEnemy;
    protected float speed;
    protected float stoppingPower;
    protected string soundName;

    protected GameObject attackInstance;
    public void SetAttackObject(GameObject p_attackObject)
    {
        attackObject = p_attackObject;
    }
    public void SetPlayerController(PlayerController p_playerScript)
    {
        playerScript = p_playerScript;
    }
    public void SetAttackDataVariables(float p_beginningTime, float p_attackTime, float p_range, string p_animationTrigger, Vector3 p_attackDirection, bool p_toEnemy, float p_speed, float p_stoppingPower, float p_attackEndTime, string p_soundName)
    {
        attackBeginningTime = p_beginningTime;
        attackTime = p_attackTime;
        attackEndTime = p_attackEndTime;
        attackRange = p_range;
        toEnemy = p_toEnemy;
        animationTrigger = p_animationTrigger;
        attackDirection = p_attackDirection;
        speed = p_speed;
        stoppingPower = p_stoppingPower;
        soundName = p_soundName;
    }
    public virtual void SetVariables(PlayerController p_playerScript, Transform p_attackParentObj, Vector3 p_targetPos, Animator anim)
    {
        playerScript = p_playerScript;
        playerAnimator = anim;
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
