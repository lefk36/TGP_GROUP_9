using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisBlades : Weapon
{
    public override void ReadInput(string button, float timeHeld)
    {
        //process the button string
        if (button == "BasicAttack")
        {
            currentAttackState = new AttackState();
            currentAttackState.StartAttack(this, AttackState.AttackType.BasicPress);
        }
    }
    protected override void Attack()
    {
        base.Attack();
         //if current attack state exists then send the button string there there, if not then create a new one
    }

}
