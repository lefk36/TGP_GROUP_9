using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ItemClick : MonoBehaviour
{
    public event Action<ItemClick, int> onItemClicked;

    [SerializeField] private Button m_Button;
    [SerializeField] private Image m_Image;
    [SerializeField] private TextMeshProUGUI m_SizeText;

    private ItemScript m_StackRef;
    private int m_ID;

    public void Init(ItemScript stackRef,int inID)
    {
        m_StackRef = stackRef;
        m_ID = inID;
        if(m_StackRef != null)
        {
            m_Button.interactable = true;
            m_Image.enabled = true;
            m_Image.sprite = m_StackRef.ItemIcon;
           
        }
    }

    private void InventoryItemClicked(ItemScript item, int id)
    {
        Debug.Log($"Cliked on ID: {id}");
    }
}
