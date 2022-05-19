using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="Attack Data")]
public class AttackData : ScriptableObject
{
    [SerializeField] private MonoScript thisAttackState;
    [SerializeField] private GameObject attackObject;
    [HideInInspector] public AttackState state;
    public string attackName;
    private AttackData transitionAttackData;
    public List<AttackData> chainableAttacks;
    public float attackBeginningTime = 0;
    public float attackTime = 0;
    public float attackRange = 1;
    [Header("Travelling Attacks")]
    public bool toEnemy;
    public Vector3 attackDirection;
    public float speed;
    public float stoppingPower = 1;
    [Header("Animation")]
    protected Animator playerAnimator;
    public string animationTriggerName;
    
    private void OnEnable()
    {
        if (thisAttackState != null)
        {
            System.Type type = thisAttackState.GetClass();
            var this_stateInstance = (AttackState)System.Activator.CreateInstance(type);
            state = this_stateInstance;
        }
        if (state != null) {
            if (attackObject != null)
            {
                state.SetAttackObject(attackObject);
            }
            attackDirection = attackDirection.normalized;
            state.SetAttackDataVariables(attackBeginningTime, attackTime, attackRange, animationTriggerName, attackDirection, toEnemy, speed, stoppingPower);
        }
    }
    public bool airRequired;
    public float timeHeldRequired;
    public string buttonRequired;

    public virtual bool ChainCombo(string button, bool air)
    {
        foreach(AttackData possibleAttack in chainableAttacks)
        {
            if(button == possibleAttack.buttonRequired && air == airRequired)
            {
                if(transitionAttackData == null)
                {
                    transitionAttackData = possibleAttack;
                }
                return true;
            }
        }
        return false;
    }
    public virtual bool ChainCombo(string button, float length, bool air)
    {
        foreach (AttackData possibleAttack in chainableAttacks)
        {
            if(length > possibleAttack.timeHeldRequired && button == possibleAttack.buttonRequired && air == possibleAttack.airRequired)
            {
                if (transitionAttackData == null)
                {
                    transitionAttackData = possibleAttack;
                }
                return true;
            }
        }
        return false;
    }
    public virtual AttackData CheckTransitions()
    {
        //when the attack is finished, it changes the variable returned in this function (if a combo chain happened)
        AttackData returnValue;
        if(!state.completed)
        {
            return null;
        }
        else
        {
            if(transitionAttackData != null)
            {
                returnValue = transitionAttackData;
                transitionAttackData = null;
                return returnValue;
            }
            else
            {
                return null;
            }
        }
    }
    public virtual void CancelAttack(Weapon caller)
    {
        state.CancelAttack(caller);
        transitionAttackData = null;
    }

}
