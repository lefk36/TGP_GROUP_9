using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStandingAttack : AttackState
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
        Debug.Log("Normal Attack");
        yield return new WaitForSeconds(1.0f);
        completed = true;
        yield return null;
    }
}
