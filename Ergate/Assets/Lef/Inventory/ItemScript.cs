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
    public int id;

    public Sprite ItemIcon;
    /// <summary>
    /// Sets the type of an item based on an enum
    /// </summary>
    public Item type;
    [TextArea(15,20)]
    // Description of Items on Screen
    public string description;

    public my_Item CreateItem()
    {
        my_Item newItem = new my_Item(this);
        return newItem;
    }
}

[System.Serializable]
public class my_Item
{
    public string Name;
    public int Id;
    public my_Item(ItemScript item)
    {
        Name = item.name;
        Id = item.id;

    }
}