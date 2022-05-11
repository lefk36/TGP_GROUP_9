using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState
{
    [HideInInspector] public bool completed = false;

    public virtual void StartAttack(Weapon caller)
    {
        //start the coroutine on the caller MonoBehaviour
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
        //stop the coroutine reference on the caller;
    }
    protected virtual IEnumerator AttackCoroutine()
    {
        yield return null;
    }
}
