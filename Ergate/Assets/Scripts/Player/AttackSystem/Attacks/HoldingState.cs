using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingState : AttackState
{

    protected override IEnumerator AttackCoroutine()
    {
        completed = false;
        while (!Input.GetButtonUp("BasicAttack"))
        {
            Debug.Log("Holding..");
            yield return null;
        }
        completed = true;
    }
}
