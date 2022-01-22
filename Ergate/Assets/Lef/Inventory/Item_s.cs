using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Item_s : MonoBehaviour, ISerializationCallbackReceiver
{
    // variable created to pass the current item being added to the inventory
    public ItemScript s_item;

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = s_item.ItemIcon;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }
}
