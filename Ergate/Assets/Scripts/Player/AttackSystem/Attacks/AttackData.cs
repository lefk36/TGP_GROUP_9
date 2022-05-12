using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="Attack Data")]
public class AttackData : ScriptableObject
{
    [SerializeField] private MonoScript thisAttackState;
    [SerializeField] private GameObject attackObject;
    [HideInInspector] public AttackState state;
    public string attackName;
    private AttackData transitionAttackData;
    public List<AttackData> chainableAttacks;
    private void OnEnable()
    {
        System.Type type = thisAttackState.GetClass();
        var this_stateInstance = (AttackState)System.Activator.CreateInstance(type);
        state = this_stateInstance;
        state.SetAttackObject(attackObject);
    }
    public bool holdingRequired;
    public float timeHeldRequired;
    public string buttonRequired;

    public virtual void ChainCombo(AttackData p_transitionAttackData)
    {
        transitionAttackData = p_transitionAttackData;
    }
    public virtual AttackData CheckTransitions()
    {
        //when the attack is finished, it changes the variable returned in this function (if a combo chain happened)
        if(!state.completed)
        {
            return null;
        }
        else
        {
            return transitionAttackData;
        }
    }

}