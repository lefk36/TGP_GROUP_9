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
        if (playerControllerScript.readyForAction)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                timeSinceLastInput = 0;
                SendTimeUpdate(timeSinceLastInput);
                SendAttack("Fire1");
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                timeSinceLastInput = 0;
                SendTimeUpdate(timeSinceLastInput);
                SendAttack("Fire2");
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            SendHoldUpdate(false, "Fire1");
        }
        if (Input.GetButtonUp("Fire2"))
        {
            SendHoldUpdate(false, "Fire2");
        }
        SendTimeUpdate(timeSinceLastInput);

    }
    void SendAttack(string button)
    {
        for (int i = 0; i < wheelControllerScript.weaponScripts.Count; i++)
        {
            if (wheelControllerScript.weaponScripts[i].GetType() == wheelControllerScript.activeWeaponType)  //look through the list of weapons and send the attack to the active one
            {
                wheelControllerScript.weaponScripts[i].Attack(button);
            }
        }
    }
    void SendHoldUpdate(bool state, string button)
    {
        for (int i = 0; i < wheelControllerScript.weaponScripts.Count; i++)
        {
            if (wheelControllerScript.weaponScripts[i].GetType() == wheelControllerScript.activeWeaponType)  //look through the list of weapons and send the update to the active one
            {
                wheelControllerScript.weaponScripts[i].SetCheckHoldingBool(false, button);
            }
        }
    }
    void SendTimeUpdate(float time)
    {
        for (int i = 0; i < wheelControllerScript.weaponScripts.Count; i++)
        {
            wheelControllerScript.weaponScripts[i].SetTimeSinceLastInput(time);
        }
    }
}
 