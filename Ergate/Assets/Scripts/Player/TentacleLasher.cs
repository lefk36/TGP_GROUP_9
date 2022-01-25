using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleLasher : Weapon
{
    public override void Attack(string button)
    {
        Debug.Log("Tentacle Lasher attacks with: " + button);
    }
}
