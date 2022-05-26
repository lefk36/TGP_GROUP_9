using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.UI
{
    public class UIInnventoryPage : MonoBehaviour
    {
        [SerializeField] private ItemScript itemPrefab;

        [SerializeField] private RectTransform contentPanel;

        [SerializeField] private InventoryDescription itemDescription;

        [SerializeField] private MouseFollow mouseFollow;

        [SerializeField]
        List<ItemScript> listOfItems = new List<ItemScript>();

        private int currentyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequsted, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            Hide();
            mouseFollow.Toggle(false);
            itemDescription.ResetDescription();
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                ItemScript uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);

                listOfItems.Add(uiItem);
                //events for item functions
                uiItem.OnItemClicked += SignalItemSelection;
                uiItem.OnItemOnItemBeginDrag += SignalBeginDrag;
                uiItem.OnItemEndDrag += SignalEndDrag;
                uiItem.OnItemSwap += SignalSwap;
                uiItem.OnRightMouseClicked += SignalShowItemActions;
            }
        }

        internal void ResetAllitems()
        {
            foreach (var item in listOfItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfItems.Count > itemIndex)
            {
                listOfItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void SignalShowItemActions(ItemScript obj)
        {
            int index = listOfItems.IndexOf(obj);
            if(index == -1)
            {
                return;
            }
            OnItemActionRequsted?.Invoke(index);
        }

        private void SignalSwap(ItemScript obj)
        {
            int index = listOfItems.IndexOf(obj);
            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(currentyDraggedItemIndex, index);
            SignalItemSelection(obj);
        }

        private void ResetDraggedItem()
        {
            mouseFollow.Toggle(false);
            currentyDraggedItemIndex = -1;
        }

        private void SignalEndDrag(ItemScript obj)
        {
            ResetDraggedItem();
        }

        private void SignalBeginDrag(ItemScript obj)
        {
            int index = listOfItems.IndexOf(obj);
            if (index == -1)
                return;
            currentyDraggedItemIndex = index;
            SignalItemSelection(obj);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollow.Toggle(true);
            mouseFollow.SetData(sprite, quantity);
        }

        private void SignalItemSelection(ItemScript obj)
        {

            int index = listOfItems.IndexOf(obj);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();


        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        private void DeselectAllItems()
        {
            foreach (ItemScript item in listOfItems)
            {
                item.Deselect();
            }
        }
    }
}
