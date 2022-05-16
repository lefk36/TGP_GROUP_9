using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingAttack : AttackState
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
        Debug.Log("Travelling attack");
        completed = true;
        yield return null;
    }
}
