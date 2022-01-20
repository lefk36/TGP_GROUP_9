using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abil_Manager : MonoBehaviour
{

    /// <summary>
    /// Ability index
    /// 0 = Dash
    /// 1 = double jump
    /// 2 = grappel
    /// 3 = liquid
    /// 4 = ...
    /// </summary>
    [Header("Abilities")]
    public List<Behaviour> m_AllAbilities = new List<Behaviour>();
    public List<Behaviour> m_UnlockedAbilities = new List<Behaviour>();

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public void enableAbility(int abilityIndex)
    {
        m_AllAbilities[abilityIndex].enabled = true;
        m_UnlockedAbilities.Add(m_AllAbilities[abilityIndex]);
    }

    public void disableAbility(int abilityIndex) 
    {
        m_AllAbilities[abilityIndex].enabled = false;
        m_UnlockedAbilities.Remove(m_AllAbilities[abilityIndex]);
    }
}
