using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
public class InventoryItems : ScriptableObject, ISerializationCallbackReceiver
{

    public string savePath;
   
    private ItemDataBase dataBase;
    /// <summary>
    /// A list that creates spots for use for each item in the game 
    /// </summary>
    public Inventory Container;
    

    // function that adds item based on a count of the list and creates a spot for each number accordingly.Also counts the amount of each item
    public void AddItem(ItemObject m_item, int m_amount)
    {
        bool itemIsHeld = false;
        for(int i = 0; i < Container.Items.Count; i++)
        {
            if(Container.Items[i].item == m_item)
            {
                Container.Items[i].AddAmount(m_amount);                
                itemIsHeld = true;
                return;
            }
        }
        if (!itemIsHeld)
        {
            Container.Items.Add(new InventorySpot(m_item.id,m_item, m_amount));
        }
    }

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

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            //BinaryFormatter binFor = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(binFor.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    public void Clear()
    {
        Container = new Inventory();

    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Items.Count; i++) 

            Container.Items[i].item = dataBase.GetItem[Container.Items[i].ID];

    }

    public void OnBeforeSerialize()
    {

    }
}

[System.Serializable]
public class Inventory
{
    public List<InventorySpot> Items = new List<InventorySpot>();
}

// Creates the individual spot 
[System.Serializable]
public class InventorySpot
{
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySpot(int m_ID,ItemObject m_item, int m_amount)
    {
        ID = m_ID;
        item = m_item;
        amount = m_amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

  
}
