using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCannon : Weapon
{
    public override void Attack(string button)
    {
        Debug.Log("Spike Cannon attacks with: " + button);
    }

}
