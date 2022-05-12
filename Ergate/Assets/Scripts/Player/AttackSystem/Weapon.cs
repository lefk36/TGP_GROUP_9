using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class Weapon : MonoBehaviour
{
    protected bool inCombo = false;
    protected AttackData currentAttackData;

    //base data - input is sent to this state when not in combo
    [SerializeField] AttackData baseAttackData;

    //Player Object -> Sent to the attack behaviours
    GameObject player;

    private void Start()
    {
        currentAttackData = baseAttackData;
        player = transform.parent.gameObject;
    }
    public virtual bool ReadInput(string button, float timeHeld)
    {
        //process the button string and time its held

        //returns true if input matched an attack
        bool inputDetected = false;

        //if the current attack state is not matching the base, then the player is inside a combo
        bool inCombo = false;
        if (currentAttackData.name != baseAttackData.name)
        {
            inCombo = true;
        }

        //get the attack data of each of the state's chainable attacks and determine whether there are holding buttons present
        bool holdingAttacksPresentBasic = false;
        bool holdingAttacksPresentAlternative = false;
        foreach (AttackData possibleAttack in currentAttackData.chainableAttacks)
        {
            //this input detection applies to attacks which require holding, so the check if any buttons require it is made first
            if(possibleAttack.buttonRequired == "BasicAttack")
            {
                if (!holdingAttacksPresentBasic)
                {
                    holdingAttacksPresentBasic = possibleAttack.holdingRequired;
                }
            }
            else if(possibleAttack.buttonRequired == "AlternativeAttack")
            {
                if (!holdingAttacksPresentAlternative)
                {
                    holdingAttacksPresentAlternative = possibleAttack.holdingRequired;
                }
            }

        }
        //now that it's determined whether a holding attack was present or not, check if input matches the attack
        foreach (AttackData possibleAttack in currentAttackData.chainableAttacks)
        {
            if(possibleAttack.buttonRequired == "BasicAttack") //If no buttons require holding, then the input is detected on instant buttons too.
            {
                if (!holdingAttacksPresentBasic && button == possibleAttack.buttonRequired)
                {
                    //input matches, start the attack.
                    inputDetected = true;
                    if (!inCombo)
                    {
                        currentAttackData = possibleAttack;
                        currentAttackData.state.StartAttack(this, player);
                    }
                    else
                    {
                        currentAttackData.ChainCombo(possibleAttack);
                    }
                }
            }
            else if(possibleAttack.buttonRequired == "AlternativeAttack")
            {
                if (!holdingAttacksPresentAlternative && button == possibleAttack.buttonRequired)
                {
                    //input matches, start the attack.
                    inputDetected = true;
                    if (!inCombo)
                    {
                        currentAttackData = possibleAttack;
                        currentAttackData.state.StartAttack(this, player);
                    }
                    else
                    {
                        currentAttackData.ChainCombo(possibleAttack);
                    }
                }
            }
            if (possibleAttack.holdingRequired)
            {
                if (button == possibleAttack.buttonRequired && timeHeld > possibleAttack.timeHeldRequired)
                {
                    // input matches, start the attack
                    inputDetected = true;
                    if (!inCombo)
                    {
                        currentAttackData = possibleAttack;
                        currentAttackData.state.StartAttack(this, player);
                    }
                    else
                    {
                        currentAttackData.ChainCombo(possibleAttack);
                    }
                }
            }
        }
        return inputDetected;
    }
    public virtual void ReadInputUp(string button)
    {
        //process the button string, for attacks which happen when the button is released
        inCombo = false;
        if (currentAttackData.name != baseAttackData.name)
        {
            inCombo = true;
        }
        foreach (AttackData possibleAttack in currentAttackData.chainableAttacks)
        {
            if (button == possibleAttack.buttonRequired)
            {
                //input matches, start the attack.
                if (!inCombo)
                {
                    currentAttackData = possibleAttack;
                    currentAttackData.state.StartAttack(this, player);
                }
                else
                {
                    currentAttackData.ChainCombo(possibleAttack);
                }
            }
        }
    }
    protected virtual void Update()
    {
        //if the attack state is flagged as completed, check the transitions. If transitions present, go to new attack, If no transitions, call cancel.
        if (currentAttackData.name != baseAttackData.name)
        {
            if (currentAttackData.state.completed)
            {
                if(currentAttackData.CheckTransitions() == null)
                {
                    Cancel();
                }
                else
                {
                    currentAttackData = currentAttackData.CheckTransitions();
                    currentAttackData.state.StartAttack(this, player);
                }
            }
        }
    }
    public virtual void Cancel()
    {
        if (currentAttackData.name != baseAttackData.name)
        {
            currentAttackData.state.CancelAttack(this);
            currentAttackData = baseAttackData;
            inCombo = false;
        }
    }
}
