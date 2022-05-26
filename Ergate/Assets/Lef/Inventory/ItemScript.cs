using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class ItemScript : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image itemImage;

        [SerializeField] private TMP_Text quantityText;

        [SerializeField] private Image frameImage;

        public event Action<ItemScript> OnItemClicked, OnItemOnItemBeginDrag, OnItemEndDrag, OnRightMouseClicked, OnItemSwap;


        private bool empty = true;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            this.itemImage.gameObject.SetActive(false);
            empty = true;
        }

        public void Deselect()
        {
            frameImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.quantityText.text = quantity + "";
            empty = false;
        }

        public void Select()
        {
            frameImage.enabled = true;
        }


        public void OnPointerClick(PointerEventData pointerData)
        {

            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseClicked?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty == true)
            {
                return;
            }
            OnItemOnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Unity nedds this function for this to work, don't delete pls
        }
    }
}

