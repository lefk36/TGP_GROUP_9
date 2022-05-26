using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventoryItems inventoryData;

    private void OnTriggerEnter(Collider other)
    {
        ItemPick item = other.GetComponent<ItemPick>();
        if(item != null)
        {
            int collector = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if(collector == 0)
            {
                item.DestroyItem();
            }
            else
            {
                item.Quantity = collector;
            }
        }
    }

   
}
