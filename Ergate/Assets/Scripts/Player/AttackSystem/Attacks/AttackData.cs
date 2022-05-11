using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="Attack Data")]
public class AttackData : ScriptableObject
{
    [SerializeField] private MonoScript thisAttackState;
    [HideInInspector] public AttackState state;
    public List<AttackData> chainableAttacks;
    private void OnEnable()
    {
        System.Type type = thisAttackState.GetClass();
        var this_stateInstance = (AttackState)System.Activator.CreateInstance(type);
        state = this_stateInstance;
    }
    public bool holdingRequired;
    public float timeHeldRequired;
    public string buttonRequired;
}
