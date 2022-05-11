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
        Weapon activeWeapon = weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex];
        if (Input.GetButton("BasicAttack"))
        {
            basicHoldTime += Time.deltaTime;
            if (activeWeapon.ReadInput("BasicAttack", basicHoldTime))//if input matched an attack
            {
                basicHoldTime = 0;
            }
        }
        else if (Input.GetButton("AlternativeAttack"))
        {
            alternativeHoldTime += Time.deltaTime;
            if (activeWeapon.ReadInput("AlternativeAttack", alternativeHoldTime))
            {
                alternativeHoldTime = 0;
            }
        }
        if (Input.GetButtonUp("BasicAttack"))
        {
            basicHoldTime = 0;
            activeWeapon.ReadInputUp("BasicAttack");
        }
        else if (Input.GetButtonUp("AlternativeAttack"))
        {
            alternativeHoldTime = 0;
            activeWeapon.ReadInputUp("AlternativeAttack");
        }

    }
    public void CancelAttacks()
    {
        weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex].Cancel();
    }
}
 