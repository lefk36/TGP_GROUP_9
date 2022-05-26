using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{
    private UIController weaponWheelController;
    private PlayerController playerControllerScript;
    private PlayerPoiseAndHealth playerHealthScript;
    public audioController m_audioController;
    private float basicHoldTime = 0;
    private float alternativeHoldTime = 0;
    private float noInputTime = 0;
    bool inAir = false;
    bool lastInAir = false;
    private void Start()
    {
        weaponWheelController = GetComponent<UIController>();
        playerControllerScript = transform.parent.GetComponent<PlayerController>();
        playerHealthScript = transform.parent.GetComponent<PlayerPoiseAndHealth>();
    }
    void Update()
    {
        UpdateInAir();
        Weapon activeWeapon = weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex];
        if (!playerControllerScript.m_hasDashed && !playerHealthScript.m_IsDead)
        {
            if (Input.GetButton("BasicAttack"))
            {
                m_audioController.play("playerAttackScream");
                basicHoldTime += Time.deltaTime;
                if (activeWeapon.ReadInput("BasicHold", basicHoldTime, inAir))//if input matched an attack
                {
                    basicHoldTime = 0;
                }
            }
            else if (Input.GetButton("AlternativeAttack"))
            {
                alternativeHoldTime += Time.deltaTime;
                if (activeWeapon.ReadInput("AlternativeHold", alternativeHoldTime, inAir))
                {
                    alternativeHoldTime = 0;
                }
            }
            if (!Input.GetButton("AlternativeAttack") && !Input.GetButton("BasicAttack"))
            {
                noInputTime += Time.deltaTime;
                if (activeWeapon.ReadInput("No Input", noInputTime, inAir))
                {
                    noInputTime = 0;
                }
            }
            else
            {
                noInputTime = 0;
            }
            if (Input.GetButtonDown("BasicAttack"))
            {
                activeWeapon.ReadInputInstant("BasicDown", inAir);
            }
            else if (Input.GetButtonDown("AlternativeAttack"))
            {
                activeWeapon.ReadInputInstant("AlternativeDown", inAir);
            }
            else if (Input.GetButtonUp("BasicAttack"))
            {
                basicHoldTime = 0;
                activeWeapon.ReadInputInstant("BasicUp", inAir);
            }
            else if (Input.GetButtonUp("AlternativeAttack"))
            {
                alternativeHoldTime = 0;
                activeWeapon.ReadInputInstant("AlternativeUp", inAir);
            }
        }

    }
    private void UpdateInAir()
    {
        inAir = !playerControllerScript.isOnGround;
        if (lastInAir != inAir)
        {
            ResetHoldingTimes();
            lastInAir = inAir;
        }
    }
    public void CancelAttacks()
    {
        weaponWheelController.weaponScripts[weaponWheelController.activeWeaponIndex].Cancel();
        ResetHoldingTimes();
    }
    public void ResetHoldingTimes()
    {
        basicHoldTime = 0;
        alternativeHoldTime = 0;
        noInputTime = 0;
    }
}
