using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{

    private WeaponWheelController wheelControllerScript;
    private PlayerController playerControllerScript;
    float timeSinceLastInput;
    private void Start()
    {
        timeSinceLastInput = 0;
        wheelControllerScript = gameObject.GetComponent<WeaponWheelController>();
        playerControllerScript = transform.parent.GetComponent<PlayerController>();
    }
    void Update()
    {
        timeSinceLastInput += Time.deltaTime;
        if (playerControllerScript.readyForAction) //check if player is in the middle of another action
        {
            if (Input.GetButtonDown("Fire1"))
            {
                timeSinceLastInput = 0;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                timeSinceLastInput = 0;
            }
        }


    }
    void SendAttack(ButtonType button)
    {
        for (int i = 0; i < wheelControllerScript.weaponScripts.Count; i++)
        {
            if (i == wheelControllerScript.activeWeaponIndex)  //look through the list of weapons and send the attack to the active one
            {
                wheelControllerScript.weaponScripts[i].Attack(button);
            }
        }
    }


}
 