using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingAttack", menuName = "Attacks/MovingAttack")]
public class MovingAttack : AttackMove
{
    public Vector3 attackDirection;
    public float movementForce;
    protected override IEnumerator DoAttack()
    {
        yield return null;
    }
}
