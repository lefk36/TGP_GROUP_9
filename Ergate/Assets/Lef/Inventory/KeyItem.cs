using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon Object", menuName = "Inventory System/Items/KeyItem")]
public class KeyItem : ItemScript
{
    void Awake()
    {
        type = Item.Key;
    }
}
