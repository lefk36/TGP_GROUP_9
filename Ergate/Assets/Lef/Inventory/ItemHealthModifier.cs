using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemHealthModifier : CharacterModifications
{
    public override void AffectCharacterStats(GameObject character, float val)
    {
        float health = character.GetComponent<PlayerPoiseAndHealth>().m_currentPlayerHealth;

        HealthBar.instance.AddHealth((int)val);
        health += val;

        Debug.Log("Heal Amount : " + val);
    }
}
