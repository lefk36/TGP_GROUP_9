using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    Weapon,
    Potion,
    Key,
    Default
}

public abstract class ItemScript : ScriptableObject
{
    public GameObject itemPrefab;
    /// <summary>
    /// Sets the type of an item based on an enum
    /// </summary>
    public Item type;
    [TextArea(15,20)]
    // Description of Items on Screen
    public string description;

}
