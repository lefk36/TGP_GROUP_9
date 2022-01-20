using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public List<InventorySpot> Container = new List<InventorySpot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        dataBase = (ItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Lef/Inventory/ScriptableObjects/Items/DataBase/BasicDataBase.asset", typeof(ItemDataBase));

#endif
    }

    // function that adds item based on a count of the list and creates a spot for each number accordingly.Also counts the amount of each item
    public void AddItem(ItemScript m_item, int m_amount)
    {
        bool itemIsHeld = false;
        for(int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == m_item)
            {
                Container[i].AddAmount(m_amount);                
                itemIsHeld = true;
                return;
            }
        }
        if (!itemIsHeld)
        {
            Container.Add(new InventorySpot(dataBase.GetID[m_item],m_item, m_amount));
        }
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter binFor = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        binFor.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter binFor = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binFor.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)

            Container[i].item = dataBase.GetItem[Container[i].ID];

    }

    public void OnBeforeSerialize()
    {

    }
}

// Creates the individual spot 
[System.Serializable]
public class InventorySpot
{
    public int ID;
    public ItemScript item;
    public int amount;
    public InventorySpot(int m_ID,ItemScript m_Item, int m_amount)
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
