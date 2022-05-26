using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;

public class MouseFollow : MonoBehaviour
{

    [SerializeField] Canvas canvas;

    [SerializeField] private ItemScript item;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<ItemScript>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }

    private void Update()
    {
        Vector2 position;
        //Helps with worls space transform
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled{val}");
        gameObject.SetActive(val);
    }


}
