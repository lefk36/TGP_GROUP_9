using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemHealthModifier : CharacterModifications
{
    public override void AffectCharacterStats(GameObject character, float val)
    {
        float health;
        health = character.GetComponent<PlayerPoiseAndHealth>().m_currentPlayerHealth;

        health += val;
        HealthBar.instance.AddHealth(val);
        Debug.Log("Heal Amount : " + val);
    }
}
