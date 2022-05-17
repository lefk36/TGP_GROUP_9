using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingState : AttackState
{
    public override void StartAttack(Weapon caller)
    {
        base.StartAttack(caller);
    }
    public override void CancelAttack(MonoBehaviour caller)
    {
        base.CancelAttack(caller);
    }
    protected override IEnumerator AttackCoroutine()
    {
        completed = false;
        Debug.Log("Paused..");
        yield return new WaitForSeconds(attackTime);
        completed = true;
        yield return null;
    }
}
