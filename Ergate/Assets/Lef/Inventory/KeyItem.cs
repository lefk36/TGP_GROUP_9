using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new Weapon Object", menuName = "Inventory System/Items/KeyItem")]
public class KeyItem : ItemScript
{


    void OnButtonClick(string img_name)
    {
        Debug.Log("Player has the key" + img_name);
        
    }
}
