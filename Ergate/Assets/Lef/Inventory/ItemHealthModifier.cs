using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemHealthModifier : CharacterModifications
{
    public override void AffectCharacterStats(GameObject character, float val)
    {
        //HealthBar.instance.AddHealth((int)val);
        Debug.Log("Heal Amount : " + val);
    }
}
