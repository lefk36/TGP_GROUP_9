using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //reference to player movement to stop it during attack
    protected PlayerController playerControllerScript;

    //variables from AttackInput script
    protected ButtonType lastAttackButton;

    //the coroutine holding the currently running combo
    protected IEnumerator activeComboCoroutine;

    //combo conditionals
    protected bool comboActive = false;
    protected float holdStartTime;
    protected float holdElapsedTime = 0.0f;

    [SerializeField]
    protected AttackMove[] m_Moves;

    public virtual void Attack(ButtonType button) //function choosing which combo to run based on attack input and combo state. Overriden by derived classes
    {

    }
    public void SetPlayerController(PlayerController controller) //the weapon wheel will call this function when adding all the weapons to weapons list
    {
        playerControllerScript = controller;
    }

}
