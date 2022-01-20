using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerInventory : MonoBehaviour
{
    public InventoryItems inventory;

    // Adds Item to player inventory when player collides with it
  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }
    }

    // Clears the inventory when game is exited
    private void OnApplicationQuit()
    {
        //inventory.Container.Items.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        var s_item = other.GetComponent<Item_s>(); 
        if (s_item)
        {
            inventory.AddItem(new ItemObject(s_item.s_item), 1);
            Destroy(other.gameObject);
            Debug.Log("Collided");
        }
    }
}
