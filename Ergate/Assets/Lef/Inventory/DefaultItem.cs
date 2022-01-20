using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Default Object", menuName = "Inventory System/Items/Default")]
public class DefaultItem : ItemScript
{
    // class for creating the Default Items

    public void Awake()
    {
        type = Item.Default;
    }
}
