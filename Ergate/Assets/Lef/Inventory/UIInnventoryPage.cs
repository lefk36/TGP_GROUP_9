using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIInnventoryPage : MonoBehaviour
{
    [SerializeField] private ItemScript itemPrefab;

    [SerializeField] private RectTransform contentPanel;

    [SerializeField] private InventoryDescription itemDescription;

    [SerializeField] private MouseFollow mouseFollow;

    List<ItemScript> listOfItems = new List<ItemScript>();

    //sprite image
    public Sprite image;
    public int quantity;
    public string title, description;

    private int currentyDraggedItemIndex = -1;

    private void Awake()
    {
        Hide();
        mouseFollow.Toggle(false);
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for(int i = 0; i< inventorySize; i++)
        {
            ItemScript uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel, false);
            
            listOfItems.Add(uiItem);
            //events for item functions
            uiItem.OnItemClicked += SignalItemSelection;
            uiItem.OnItemOnItemBeginDrag += SignalBeginDrag;
            uiItem.OnItemEndDrag += SignalEndDrag;
            uiItem.OnItemSwap += SignalSwap;
            uiItem.OnRightMouseClicked += SignalShowItemActions;
        }
    }

    private void SignalShowItemActions(ItemScript obj)
    {
      
    }

    private void SignalSwap(ItemScript obj)
    {
        //int  index = listOfItems.IndexOf(obj);
        //if(index == -1)
        //{
        //    mouseFollow.Toggle(false);
        //    currentyDraggedItemIndex = -1;
        //    return;
        //}
        //listOfItems[currentyDraggedItemIndex].SetData(index == 0 ? image : otherImage, quantity);
        //listOfItems[index].SetData(index == 0 ? image : otherImage, quantity);

        //mouseFollow.Toggle(false);
        //currentyDraggedItemIndex = -1;
    }

    private void SignalEndDrag(ItemScript obj)
    {
        mouseFollow.Toggle(false);
    }

    private void SignalBeginDrag(ItemScript obj)
    {
        mouseFollow.Toggle(true);
        mouseFollow.SetData(image, quantity);
    }

    private void SignalItemSelection(ItemScript obj)
    {
        Debug.Log(obj.name);
        itemDescription.SetDescription(image, title, description);
        listOfItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfItems[0].SetData(image, quantity);
        //listOfItems[1].SetData(otherImage, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
