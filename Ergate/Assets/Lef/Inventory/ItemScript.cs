using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public  class ItemScript : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    [SerializeField] private TMP_Text quantityText;

    [SerializeField] private Image frameImage;

    public event Action<ItemScript> OnItemClicked, OnItemOnItemBeginDrag, OnItemEndDrag, OnRightMouseClicked;


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

    private void Select()
    {
        frameImage.enabled = true;
    }

    public void OnBeginDrag()
    {
        if (empty)
            return;
        OnItemOnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        if(pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseClicked?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

}

