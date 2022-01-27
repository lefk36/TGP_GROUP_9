using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NormalAttack", menuName = "Attacks/NormalAttack")]
public class NormalAttack : AttackMove
{
    protected override IEnumerator DoAttack()
    {
        yield return null;
    }
}
