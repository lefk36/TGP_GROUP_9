using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
public class InventoryItems : ScriptableObject
{

    public string savePath;
   
    public ItemDataBase dataBase;
    /// <summary>
    /// A list that creates spots for use for each item in the game 
    /// </summary>

    // instance of the ObjectItems class
    public Inventory Container;


    // function that adds item based on a count of the list and creates a spot for each number accordingly.Also counts the amount of each item
    public void AddItem(my_Item m_item, int m_amount)
    {
        if(m_item.buffs.Length > 0)
        {
            SetEmptySpot(m_item, m_amount);
            return;
        }
        bool itemIsHeld = false;
        for (int i = 0; i < Container.ObjectItems.Length; i++)
        {
            if (Container.ObjectItems[i].ID == m_item.Id)
            {
                Container.ObjectItems[i].AddAmount(m_amount);
                itemIsHeld = true;
                return;
            }
        }
        SetEmptySpot(m_item, m_amount);
        if (!itemIsHeld)
        {
            
        }
    }

    public InventorySpot SetEmptySpot(my_Item m_item, int m_amount)
    {
        for(int i =0;i< Container.ObjectItems.Length; i++)
        {
            if(Container.ObjectItems[i].ID <= -1)
            {
                Container.ObjectItems[i].UpdateSpot(m_item.Id, m_item, m_amount);
                return Container.ObjectItems[i];
            }
        }// set up functionality for full inventory
        return null;
    }

    [ContextMenu("Save")]
    public void Save()
    {
        
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
       
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
        Container = (Inventory)formatter.Deserialize(stream);
        stream.Close();
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}


[System.Serializable]
public class Inventory 
{
    public InventorySpot[] ObjectItems = new InventorySpot[24];
}


// Creates the individual spot 
[System.Serializable]
public class InventorySpot
{
    public int ID = -1;
    public my_Item item;
    public int amount;
    public InventorySpot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }

    public void UpdateSpot(int m_id, my_Item m_item, int m_amount)
    {
        ID = m_id;
        item = m_item;
        amount = m_amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

  
}
