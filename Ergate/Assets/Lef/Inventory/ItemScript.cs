using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    Weapon,
    Potion,
    Key,
    Default
}

public enum Attributes
{
    Speed,
    Intellect,
    Strength,
    Defence,
    Health
}

public abstract class ItemScript : ScriptableObject
{
    public Sprite ItemIcon;
    [SerializeField]
    public ItemBuff[] buffs;

    public int id;

    /// <summary>
    /// Sets the type of an item based on an enum
    /// </summary>
    public Item type;
    [TextArea(15,20)]
    // Description of Items on Screen
    public string description;

    public my_Item CreateItem(ItemScript item)
    {
        my_Item newItem = new my_Item(this);
        return newItem;
    }

    public void OnButtonClick(string img_name)
    {
        Debug.Log(img_name);
    }
}

[System.Serializable]
public class my_Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;
    public my_Item(ItemScript item)
    {
        Name = item.name;
        Id = item.id;
        buffs = new ItemBuff[item.buffs.Length];
        for(int i =0; i< buffs.Length; i++)
        {
            buffs[i].m_attribute = item.buffs[i].m_attribute;
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
        }
    }

   
}

[System.Serializable]
public class ItemBuff
{
    public Attributes m_attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int m_min, int m_max)
    {
        min = m_min;
        max = m_max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}

