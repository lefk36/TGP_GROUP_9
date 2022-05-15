using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class Weapon : MonoBehaviour
{
    protected AttackData currentAttackData;

    //base data - input is sent to this state when not in combo
    [SerializeField] AttackData baseAttackData;

    //Player Object -> Sent to the attack behaviours
    GameObject player;
    PlayerController controllerScript;
    AttackInput inputComponent;

    private void Start()
    {
        currentAttackData = baseAttackData;
        baseAttackData.state.completed = true;
        player = transform.parent.gameObject;
        controllerScript = player.GetComponent<PlayerController>();
        inputComponent = GetComponent<AttackInput>();
    }
    public virtual bool ReadInput(string button, float timeHeld, bool air)
    {
        //process the button string and time its held

        //returns true if input matched an attack
        return currentAttackData.ChainCombo(button, timeHeld, air);
    }
    public virtual void ReadInputInstant(string button, bool air)
    {
        //process the button string
        currentAttackData.ChainCombo(button, air);
    }
    protected virtual void LateUpdate()
    {
        //if air state does not match the current attacks, call cancel
        if(currentAttackData.airRequired != !controllerScript.isOnGround)
        {
            Cancel();
        }

        //if the attack state is flagged as completed, check the transitions. If transitions present, go to new attack, If no transitions, call cancel.
        if (currentAttackData.state.completed)
        {
            AttackData newData = currentAttackData.CheckTransitions();
            if (newData == null)
            {
                Cancel();
            }
            else
            {
                currentAttackData = newData;
                StopAttack();
                currentAttackData.state.StartAttack(this, player);
                inputComponent.ResetHoldingTimes();
            }
        }
    }
    public virtual void StopAttack()
    {
        if (currentAttackData.name != baseAttackData.name)
        {
            currentAttackData.CancelAttack(this);
        }
    }
    public virtual void Cancel()
    {
        if (currentAttackData.name != baseAttackData.name)
        {
            currentAttackData.CancelAttack(this);
            currentAttackData = baseAttackData;
            inputComponent.ResetHoldingTimes();
        }
    }
}
