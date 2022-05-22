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
    Transform attackDirectionObj;
    PlayerController controllerScript;
    Animator playerAnimator;
    EnemiesCameraLock lockScript;
    AttackInput inputComponent;

    private void Start()
    {
        playerAnimator = transform.GetChild(0).GetComponent<Animator>();
        currentAttackData = baseAttackData;
        baseAttackData.state.completed = true;
        attackDirectionObj = transform.parent.Find("Attack Direction");
        controllerScript = transform.parent.GetComponent<PlayerController>();
        lockScript = transform.parent.Find("Camera Centre").GetChild(0).GetComponent<EnemiesCameraLock>();
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
        //if player is on ground and attack's air is required, call cancel
        if(currentAttackData.airRequired && controllerScript.isOnGround)
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
                Vector3 targetPos = FindTargetPosition(currentAttackData);
                currentAttackData.state.SetVariables(controllerScript, attackDirectionObj, targetPos, playerAnimator);
                currentAttackData.state.StartAttack(this);
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
            currentAttackData.state.SetPlayerController(controllerScript);
            currentAttackData.CancelAttack(this);
            controllerScript.stickToAttack = false;
            controllerScript.lockAttackDirection = false;
            controllerScript.lockMovement = false;

            controllerScript.rigidbody.velocity = controllerScript.rigidbody.velocity / 2;
            controllerScript.lockFalling = false;
            playerAnimator.SetTrigger("StopAttacking");
            currentAttackData = baseAttackData;
            inputComponent.ResetHoldingTimes();
        }
    }
    protected virtual Vector3 FindTargetPosition(AttackData data)
    {
        if (data.toEnemy && controllerScript.cameraLockedToTarget)
        {
            return lockScript.m_TargetableEnemies[lockScript.m_TargetableEnemyIndex].transform.position;
        }
        else
        {
            return attackDirectionObj.position + (attackDirectionObj.rotation * data.attackDirection * data.attackRange);
        }
    }
}
