using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousHoldingAttack : AttackState
{
    protected override IEnumerator AttackCoroutine()
    {
        completed = false;
        while (!Input.GetButtonUp("AlternativeAttack"))
        {
            Debug.Log("Shredding..");
            if (!Input.GetButton("AlternativeAttack"))
            {
                break;
            }
            yield return null;
        }
        completed = true;
        yield return null;
    }
}
