using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisBlades : Weapon
{
    public override void Attack(string button)
    {
        lastAttackButton = button;
        
        if(!comboActive)
        {
            attacking = true;
            comboActive = true;
            checkForHolding = true;
            Debug.Log("Coroutine started");
            comboStartTime = Time.time;
            StartCoroutine("ComboWaitForInput");
        }
        else
        {
            attacking = true;
            checkForHolding = true;
        }
    }
    IEnumerator ComboWaitForInput()
    {
        while (timeSinceLastAttackInput < 0.75f)
        {
            comboElapsedTime = Time.time - comboStartTime;
            if (attacking)
            {
                attacking = false;
                playerControllerScript.readyForAction = false;
                playerControllerScript.lockMovement = true;
                playerControllerScript.lockFalling = true;
                playerControllerScript.lockAttackDirection = true;
                yield return new WaitForSeconds(0.5f);
                if (checkForHolding)
                {
                    Debug.Log("Charged attack!");
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    Debug.Log("normal attack");
                    yield return new WaitForSeconds(0.6f);
                }
                playerControllerScript.readyForAction = true;
                playerControllerScript.lockMovement = false;
                playerControllerScript.lockFalling = false;
                playerControllerScript.lockAttackDirection = false;

            }
            yield return null;
        }
        comboActive = false;
    }
}
