using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ConsumableItem_s : Item_s, IDestroyableItem, IItemAction
    {

        [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();

        public string ActionName => "Consume";

        public bool IsAction(GameObject player)
        {
            foreach(ModifierData data in modifierData)
            {
                data.statModifier.AffectCharacterStats(player, data.value);
            }
            return true;
        }
    }

    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        public string ActionName { get; }

        bool IsAction(GameObject player);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterModifications statModifier;
        public float value;
    }
}