using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HoldingAttack", menuName = "Attacks/HoldingAttack")]
public class Holding : AttackMove
{
    [HideInInspector] public bool buttonHeld;
    [HideInInspector] public bool ready = false;
    private float holdingTimeStart = 0;
    private float elapsedHoldingTime = 0;
    protected override IEnumerator DoAttack()
    {
        done = false;
        ready = false;
        holdingTimeStart = Time.time;
        while (buttonHeld)
        {
            elapsedHoldingTime = Time.time - holdingTimeStart;
            if(elapsedHoldingTime > attackLength)
            {
                ready = true;
            }
            yield return null;
        }
        done = true;
        elapsedHoldingTime = 0;
    }
    public override AttackMove GetNextMove(ButtonType buttonPressed)
    {
        if (done && ready) //only chain to next move if button was held for long enough
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
