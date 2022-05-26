using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System;
using System.Linq;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryItems : ScriptableObject
    {

        [SerializeField] private List<InventoryItem> m_inventoryItems;

        [field: SerializeField] public int Size { get; private set; } = 10;
        
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdate;


        public void Initialize()
        {
            m_inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                m_inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        internal void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInvnetoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < m_inventoryItems.Count; i++)
            {
                if (m_inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = m_inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return m_inventoryItems[itemIndex];
        }

        public int AddItem(Item_s m_item, int m_quantity)
        {
            if(m_item.IsStackable == false)
            {
                for (int i = 0; i < m_inventoryItems.Count; i++)
                {
                  
                    while (m_quantity > 0 && inventoryHasSpace() == true)
                    {
                       m_quantity -= AddNonStackableItem(m_item, 1);
                       
                    }
                    InformItemChange();
                    return m_quantity;
                }
            }
            m_quantity = AddStackableItem(m_item, m_quantity);
            InformItemChange();
            return m_quantity;
        }

        public void RemoveItem(int itemIndex, int amount)
        {
           if(m_inventoryItems.Count > itemIndex)
            {
                if (m_inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = m_inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    m_inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    m_inventoryItems[itemIndex] = m_inventoryItems[itemIndex].ChangeQuantity(reminder);

                InformItemChange();
            }
        }

        private int AddNonStackableItem(Item_s m_item, int m_quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = m_item,
                quantity = m_quantity,
            };

            for (int i=0; i< m_inventoryItems.Count; i++)
            {
                if (m_inventoryItems[i].IsEmpty)
                {
                    m_inventoryItems[i] = newItem;
                    return m_quantity;
                }
            }
            return 0;

        }

        private bool inventoryHasSpace() => m_inventoryItems.Where(item => item.IsEmpty).Any() == true;

        private int AddStackableItem(Item_s m_item, int m_quantity)
        {
           for(int i =0; i < m_inventoryItems.Count; i++)
            {
                // if item is empty not smae item we are looking for
                if (m_inventoryItems[i].IsEmpty)
                    continue;
                if(m_inventoryItems[i].item.ID == m_item.ID)
                {
                    // Calculate the amount of possible items this stack can store
                    int amountPossible = m_inventoryItems[i].item.MaxStackSize - m_inventoryItems[i].quantity;

                    //If more items than stack can store modify it
                    if(m_quantity > amountPossible)
                    {
                        m_inventoryItems[i] = m_inventoryItems[i].ChangeQuantity(m_inventoryItems[i].item.MaxStackSize);
                        m_quantity -= amountPossible;
                    }
                    // Fills the stack with the items needed for stack
                    else
                    {
                        m_inventoryItems[i] = m_inventoryItems[i].ChangeQuantity(m_inventoryItems[i].quantity + m_quantity);
                        InformItemChange();
                        return 0;
                    }
                }
            }
           // Checks for remaining item stack
           while(m_quantity > 0 && inventoryHasSpace() == true)
            {
                int newQuanitity = Mathf.Clamp(m_quantity, 0, m_item.MaxStackSize);
                m_quantity -= newQuanitity;
                AddNonStackableItem(m_item, newQuanitity);
            }
            return m_quantity;
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = m_inventoryItems[itemIndex_1];
            m_inventoryItems[itemIndex_1] = m_inventoryItems[itemIndex_2];
            m_inventoryItems[itemIndex_2] = item1;
            InformItemChange();
        }

        private void InformItemChange()
        {
            OnInventoryUpdate?.Invoke(GetCurrentInvnetoryState());
        }
    }

    //Getting reference from the stack memory !Heap
    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public Item_s item;
        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem
        {
            item = null,
            quantity = 0,
        };
    }
}