using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public bool attackInitiated;
    protected AttackState currentAttackState;

    protected virtual void Start()
    {

    }

    public virtual void ReadInput(string button, float timeHeld)
    {
        //process the button string
    }
    protected virtual void Attack()
    {
        //if current attack state exists then send the button string there, if not then create a new one
        attackInitiated = true;
    }
    protected virtual void Update()
    {
        //if the attack state is flagged as completed, stop the attack
        if (currentAttackState != null)
        {
            if (currentAttackState.completed)
            {
                Cancel();
            }
        }
    }
    public virtual void Cancel()
    {
        if (currentAttackState != null)
        {
            currentAttackState.CancelAttack(this);
            attackInitiated = false;
            currentAttackState = null;
        }
    }
}
