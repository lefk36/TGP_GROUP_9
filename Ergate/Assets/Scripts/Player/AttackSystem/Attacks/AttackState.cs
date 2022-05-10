using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour
{
    public bool completed = false;
    public float timeHeldRequired;
    //enum used as a flag to send through an AttackState object
    public enum AttackType
    {
        Null,
        BasicPress, BasicHold,
        AlternativePress, AlternativeHold,
    }
    public virtual void StartAttack(MonoBehaviour caller, AttackType buttonResponse)
    {
        caller.StartCoroutine(AttackCoroutine());
    }
    public virtual void ChainCombo(string button)
    {
        //process the button string. If the attack allows for a chain combo with the button, prepare the program to change states
    }
    public virtual AttackState CheckTransitions()
    {
        //when the attack is finished, it changes the variable returned in this function (if a combo chain happened)
        return null;
    }
    public virtual void CancelAttack(MonoBehaviour caller)
    {
        caller.StopCoroutine(AttackCoroutine());
    }
    protected virtual IEnumerator AttackCoroutine()
    {
        Debug.Log("Attack Happened!");
        completed = true;
        yield return null;
    }
}
