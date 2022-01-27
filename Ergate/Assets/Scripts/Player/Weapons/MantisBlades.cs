using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisBlades : Weapon
{

    
    public override void Attack(ButtonType button)
    {
        lastAttackButton = button;
        if (!comboActive)
        {

        }
    }
}
