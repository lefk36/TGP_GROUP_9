using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{

    //basic inventory, its a list of int array. the array position 0 is the item id and position 1 is the item instance
    public List<int[]> inventory = new List<int[]>();

    private void Start()
    {
       
    }


    public void addItem(int ID, int Instance)
    {
        int[] itemToAdd = new int[2];

        itemToAdd[0] = ID;
        itemToAdd[1] = Instance;
        inventory.Add(itemToAdd);
        Debug.Log(inventory.Count);
        
    }
    
}
