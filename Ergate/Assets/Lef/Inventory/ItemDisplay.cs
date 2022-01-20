using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    
    // variables to calculate space between the icons in the inventory
    public int spaceBetwwenX;
    public int spaceBetwwenY;
    public int columnNumber;

    Dictionary<InventorySpot, GameObject> itemsDisplayed = new Dictionary<InventorySpot, GameObject>();
    public InventoryItems inventory;

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    // Displays each item icon on the screen
    public void CreateDisplay()
    {
        for(int i=0; i< inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.itemPrefab, Vector3.zero,Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
        }
    }

    // Calculates the variables of the distance between each icon
    public Vector3 GetPosition(int i)
    {
        return new Vector3(spaceBetwwenX * (i % columnNumber), (-spaceBetwwenY * (i / columnNumber)), 0f);
    }

    // Updates the display of item icons on the UI panel of the inventory
    public void UpdateDisplay()
    {

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            { 
            itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.itemPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");

                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }
}
