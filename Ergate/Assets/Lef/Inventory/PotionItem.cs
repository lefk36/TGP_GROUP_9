using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon Object", menuName = "Inventory System/Items/Potion")]
public class PotionItem : ItemScript
{
    // class for creating potion Items

    void Awake()
    {
        type = Item.Potion;
    }
}
