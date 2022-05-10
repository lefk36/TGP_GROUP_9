using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{
    private UIController weaponWheelController;
    private float basicHoldTime = 0;
    private float alternativeHoldTime = 0;
    private void Start()
    {
        weaponWheelController = GetComponent<UIController>();
    }
    void Update()
    {
        //weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex].Attack("BasicAttack");
        if (Input.GetButton("BasicAttack"))
        {
            basicHoldTime += Time.deltaTime;
            weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex].ReadInput("BasicAttack", basicHoldTime);
        }
        else
        {
            basicHoldTime = 0;
            if (Input.GetButton("AlternativeAttack"))
            {
                alternativeHoldTime += Time.deltaTime;
                weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex].ReadInput("AlternativeAttack", alternativeHoldTime);
            }
            else
            {
                alternativeHoldTime = 0;
            }
        }
    }
    public void CancelAttacks()
    {
        weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex].Cancel();
    }
}
 