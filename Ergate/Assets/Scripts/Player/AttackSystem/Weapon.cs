using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected AttackState currentAttackState;
    public virtual void Attack(string button)
    {
        //process the button string, if current attack state exists then send it there, if not then create a new one

    }
    protected virtual void Update()
    {
        //if the attack state is flagged as completed, stop the attack
        if (currentAttackState != null)
        {
            if (currentAttackState.completed)
            {
                currentAttackState.CancelAttack(this);
                currentAttackState = null;
            }
        }
    }
    public virtual void Cancel()
    {

    }
}
