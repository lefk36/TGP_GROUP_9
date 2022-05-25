using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInnventoryPage : MonoBehaviour
{
    [SerializeField] private ItemScript itemPrefab;

    [SerializeField] private RectTransform contentPanel;

    List<ItemScript> listOfItems = new List<ItemScript>();


    public void InitializeInventoryUI(int inventorySize)
    {
        for(int i = 0; i< inventorySize; i++)
        {
            ItemScript uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel, false);
            
            listOfItems.Add(uiItem);
            uiItem.OnItemClicked += SignalItemSelection;
            uiItem.OnItemOnItemBeginDrag += SignalBeginDrag;
            uiItem.OnItemEndDrag += SignalSwap;
            uiItem.OnRightMouseClicked += SignalShowItemActions;
        }
    }

    private void SignalShowItemActions(ItemScript obj)
    {
      
    }

    private void SignalSwap(ItemScript obj)
    {
       
    }

    private void SignalBeginDrag(ItemScript obj)
    {
       
    }

    private void SignalItemSelection(ItemScript obj)
    {
        Debug.Log(obj.name);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
