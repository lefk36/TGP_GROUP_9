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
    /// <summary>
    /// Sets the item icon based on the sprite
    /// </summary>
     
    public int id;
    public Sprite itemIcon;
    public GameObject itemPrefab;

    /// <summary>
    /// Sets the type of an item based on an enum
    /// </summary>
    public Item type;
    [TextArea(15,20)]
    // Description of Items on Screen
    public string description;

}

[System.Serializable]
public class ItemObject
{
    public string Name;
    public int id;
    public ItemObject(ItemScript s_item)
    {
        Name = s_item.name;
        id = s_item.id;
    }
}
