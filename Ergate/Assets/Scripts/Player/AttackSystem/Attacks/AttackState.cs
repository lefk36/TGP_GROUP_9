using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState
{
    [HideInInspector] public bool completed = false;

    public virtual void StartAttack(Weapon caller)
    {
        //start the coroutine on the caller MonoBehaviour
        caller.StartCoroutine(AttackCoroutine());
    }
    public virtual void CancelAttack(MonoBehaviour caller)
    {
        //stop the coroutine reference on the caller;
        caller.StopCoroutine(AttackCoroutine());
    }
    protected virtual IEnumerator AttackCoroutine()
    {
        completed = true;
        yield return null;
    }
}
