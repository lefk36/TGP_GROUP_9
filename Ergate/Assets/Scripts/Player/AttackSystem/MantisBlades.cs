using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisBlades : Weapon
{
    public override void Attack(string button)
    {
        //process the button string, if current attack state exists then send it there, if not then create a new one
        if (button == "BasicAttack")
        {
            currentAttackState = new AttackState();
            currentAttackState.StartAttack(this);
        }
        else if (button == "AlternativeAttack")
        {

        }
    }
    public override void Cancel()
    {
        if(currentAttackState != null)
        {
            currentAttackState.CancelAttack(this);
        }
    }

}
