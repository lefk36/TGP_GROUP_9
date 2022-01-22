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
        bool itemIsHeld = false;
        for(int i = 0; i < Container.ObjectItems.Count; i++)
        {
            if(Container.ObjectItems[i].item.Id == m_item.Id)
            {
                Container.ObjectItems[i].AddAmount(m_amount);                
                itemIsHeld = true;
                return;
            }
        }
        if (!itemIsHeld)
        {
            Container.ObjectItems.Add(new InventorySpot(m_item.Id,m_item, m_amount));
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter binFor = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //binFor.Serialize(file, saveData);
        //file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        //if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        //{
        //    BinaryFormatter binFor = new BinaryFormatter();
        //    FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
        //    JsonUtility.FromJsonOverwrite(binFor.Deserialize(file).ToString(), this);
        //    file.Close();
        //}

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
    public List<InventorySpot> ObjectItems = new List<InventorySpot>();
}


// Creates the individual spot 
[System.Serializable]
public class InventorySpot
{
    public int ID;
    public my_Item item;
    public int amount;
    public InventorySpot(int m_ID,my_Item m_Item, int m_amount)
    {
        ID = m_ID;
        item = m_Item;
        amount = m_amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

  
}
