using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ItemDisplay : MonoBehaviour
{

    public GameObject inventoryPrefab;

    //values of startinf position for inventory item Icons
    public int startX_item;
    public int startY_item;
    // variables to calculate space between the icons in the inventory
    public int spaceBetwwenX;
    public int spaceBetweenY;
    public int columnNumber;

    Dictionary<InventorySpot, GameObject> itemsDisplayed = new Dictionary<InventorySpot, GameObject>();
    public InventoryItems inventory;

    //Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

   // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    //Displays each item icon on the screen
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.ObjectItems.Count; i++)
        {
            //Creating a variable for clearing up some code
            InventorySpot Spot = inventory.Container.ObjectItems[i];
            // Creates a gameobject prefab for each item that gets included in the database
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.GetItem[Spot.item.Id].ItemIcon;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = Spot.amount.ToString("n0");

            itemsDisplayed.Add(Spot, obj);
        }
    }

    //Calculates the variables of the distance between each icon
    public Vector3 GetPosition(int i)
    {
        return new Vector3(startX_item +(spaceBetwwenX * (i % columnNumber)),startY_item + (-spaceBetweenY * (i / columnNumber)), 0f);
    }

    //Updates the display of item icons on the UI panel of the inventory
    public void UpdateDisplay()
    {
        // Updates the gameobject prefab that gets included in the database(sprite and numbers)
        for (int i = 0; i < inventory.Container.ObjectItems.Count; i++)
        {
            //Creating a variable for clearing up some code
            InventorySpot Spot = inventory.Container.ObjectItems[i];

            if (itemsDisplayed.ContainsKey(Spot))
            {
                itemsDisplayed[Spot].GetComponentInChildren<TextMeshProUGUI>().text = Spot.amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.GetItem[Spot.item.Id].ItemIcon;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = Spot.amount.ToString("n0");

                itemsDisplayed.Add(Spot, obj);
            }
        }
    }
}
