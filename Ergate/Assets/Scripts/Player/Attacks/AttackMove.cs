using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    Basic,
    Special,
    HoldBasic,
    HoldSpecial,
    Pause
}
public class AttackMove : ScriptableObject
{
    public ButtonType buttonType;
    [SerializeField]
    protected float attackLength;
    public AttackMove[] chainableMoves;

    [HideInInspector]
    public bool done = false;

    protected virtual IEnumerator DoAttack()
    {
        yield return null;
    }

    public virtual AttackMove GetNextMove(ButtonType buttonPressed)
    {
        if (done)
        {
            foreach (AttackMove move in chainableMoves)
            {
                if (move.buttonType == buttonPressed)
                {
                    return move;
                }
            }
        }
        return null;
    }

}
