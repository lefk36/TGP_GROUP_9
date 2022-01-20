using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDataBase : ScriptableObject, ISerializationCallbackReceiver 
{
    public ItemScript[] Items;
    public Dictionary<ItemScript, int> GetID = new Dictionary<ItemScript, int>();
    public Dictionary<int, ItemScript> GetItem = new Dictionary<int, ItemScript>();

    // callback for when Unity deserializes our object
    public void OnAfterDeserialize()
    {
     
        GetID = new Dictionary<ItemScript, int>();
        GetItem  = new Dictionary<int, ItemScript>();

        for (int i = 0; i < Items.Length; i++)
        {
            GetID.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    // callback for when Unity serializes our object
    public void OnBeforeSerialize()
    {
        
    }
}
