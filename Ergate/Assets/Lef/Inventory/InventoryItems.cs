using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
public class InventoryItems : ScriptableObject
{

    public event Action<Dictionary<int, ItemScript>>
     OnInventoryUpdated;

    [SerializeField]
    private List<ItemScript> inentoryItems;

    [field: SerializeField]
    public int size { get; private set; } = 10;

}
