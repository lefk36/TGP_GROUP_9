using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState
{
    [HideInInspector] public bool completed = false;
    GameObject player;
    GameObject attackObject;
    public void SetAttackObject(GameObject p_attackObject)
    {
        attackObject = p_attackObject;
    }
    public virtual void StartAttack(Weapon caller, GameObject p_player)
    {
        //start the coroutine on the caller MonoBehaviour
        player = p_player;
        caller.StartCoroutine(AttackCoroutine());
    }
    public virtual void CancelAttack(MonoBehaviour caller)
    {
        //stop the coroutine reference on the caller;
        completed = false;
        caller.StopCoroutine(AttackCoroutine());
    }
    protected virtual IEnumerator AttackCoroutine()
    {
        completed = true;
        yield return null;
    }
}
