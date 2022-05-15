using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingAttack : AttackState
{

    protected override IEnumerator AttackCoroutine()
    {
        completed = false;
        Debug.Log("Travelling attack");
        completed = true;
        yield return null;
    }
}
