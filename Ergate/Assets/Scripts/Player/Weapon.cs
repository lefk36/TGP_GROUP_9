using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected string lastAttackButton;
    protected float timeSinceLastAttackInput;
    protected PlayerController playerControllerScript;
    protected bool checkForHolding = true;
    protected bool comboActive = false;
    protected float comboStartTime;
    protected float comboElapsedTime = 0.0f;
    protected bool attacking = false;

    public virtual void Attack(string button)
    {

    }
    public void SetPlayerController(PlayerController controller)
    {
        playerControllerScript = controller;
    }
    public void SetCheckHoldingBool(bool state, string button)
    {
        if (button == lastAttackButton)
        {
            checkForHolding = state;
        }
    }
    public void SetTimeSinceLastInput(float time)
    {
        timeSinceLastAttackInput = time;
    }
}
