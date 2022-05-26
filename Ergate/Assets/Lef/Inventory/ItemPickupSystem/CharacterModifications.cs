using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterModifications : ScriptableObject
{
    public abstract void AffectCharacterStats(GameObject character, float val);
}
