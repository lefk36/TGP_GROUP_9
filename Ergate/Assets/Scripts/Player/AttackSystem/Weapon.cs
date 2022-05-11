using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class Weapon : MonoBehaviour
{
    [HideInInspector] public bool attackInitiated;
    protected bool inCombo = false;
    protected AttackData currentAttackData;

    //base data - input is sent to this state when not in combo
    [SerializeField] AttackData baseAttackData;

    private void Start()
    {
        currentAttackData = baseAttackData;
    }
    public virtual bool ReadInput(string button, float timeHeld)
    {
        //process the button string and time its held

        //returns true if input matched an attack
        bool inputDetected = false;

        //if the current attack state is not matching the base, then the player is inside a combo
        bool inCombo = false;
        if (currentAttackData.state.GetType() != baseAttackData.state.GetType())
        {
            inCombo = true;
        }

        //get the attack data of each of the state's chainable attacks and determine whether there are holding buttons present
        bool holdingAttacksPresent = false;
        foreach (AttackData possibleAttack in currentAttackData.chainableAttacks)
        {
            //this input detection applies to attacks which require holding.
            if (!holdingAttacksPresent)
            {
                holdingAttacksPresent = possibleAttack.holdingRequired;
            }
        }
        //now that it's determined whether a holding attack was present or not, check if input matches the attack
        foreach (AttackData possibleAttack in currentAttackData.chainableAttacks)
        {
            if (!holdingAttacksPresent) //If no buttons require holding, then the input is detected on instant buttons too.
            {
                if (button == possibleAttack.buttonRequired)
                {
                    //input matches, start the attack.
                    inputDetected = true;
                }
            }
            else if (possibleAttack.holdingRequired)
            {
                if (button == possibleAttack.buttonRequired && timeHeld > possibleAttack.timeHeldRequired)
                {
                    // input matches, start the attack
                    inputDetected = true;
                }
            }
        }
        return inputDetected;
    }
    public virtual void ReadInputUp(string button)
    {
        //process the button string, for attacks which happen when the button is released
        foreach (AttackData possibleAttack in currentAttackData.chainableAttacks)
        {
            if (button == possibleAttack.buttonRequired)
            {
                //input matches, start the attack.
                bool lol123 = false;
            }
        }
    }
    protected virtual void Update()
    {
        //if the attack state is flagged as completed, stop the attack
        if (currentAttackData.state.GetType() != baseAttackData.state.GetType())
        {
            if (currentAttackData.state.completed)
            {
                Cancel();
            }
        }
    }
    public virtual void Cancel()
    {
        if (currentAttackData.GetType() != baseAttackData.GetType())
        {
            currentAttackData.state.CancelAttack(this);
            attackInitiated = false;
            currentAttackData = baseAttackData;
        }
    }
}
