using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingState : AttackState
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
        while (!Input.GetButtonUp("BasicAttack"))
        {
            Debug.Log("Holding..");
            if (!Input.GetButton("BasicAttack"))
            {
                break;
            }
            yield return null;
        }
        completed = true;
    }
}
