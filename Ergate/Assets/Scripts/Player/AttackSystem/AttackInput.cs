using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{
    private UIController weaponWheelController;
    private void Start()
    {
        weaponWheelController = GetComponent<UIController>();
    }
    void Update()
    {
        //if input is detected, send the input to currently selected weapon
        if (Input.GetButtonDown("BasicAttack"))
        {
            weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex].Attack("BasicAttack");
        }
        else if (Input.GetButtonDown("AlternativeAttack"))
        {
            weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex].Attack("AlternativeAttack");
        }

    }
    public void CancelAttacks()
    {
        weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex].Cancel();
    }
}
 