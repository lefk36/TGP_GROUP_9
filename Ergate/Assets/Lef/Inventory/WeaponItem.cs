using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class WeaponItem : ItemScript
{
    // class for creating weapon Items
    [SerializeField] private int atk = 50;
    [SerializeField] private int def = 20; 

    public void Awake()
    {
        type = Item.Weapon;
    }
}
