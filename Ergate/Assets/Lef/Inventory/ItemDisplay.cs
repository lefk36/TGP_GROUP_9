using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemDisplay : MonoBehaviour
{
    
    [SerializeField] private GameObject inventoryPrefab;

     

    //values of startinf position for inventory item Icons
    public int startX_item;
    public int startY_item;
    // variables to calculate space between the icons in the inventory
    public int spaceBetwwenX;
    public int spaceBetweenY;
    public int columnNumber;

    bool keyCheck;

    private Button iButton; 

    Dictionary<GameObject, InventorySpot> itemsDisplayed = new Dictionary<GameObject, InventorySpot>();
    public InventoryItems inventory;

   
    private ItemDataBase data;

    //Start is called before the first frame update
    void Awake()
    {
        iButton = inventoryPrefab.GetComponent<Button>();
        CreateDisplaySpot();
        
    }

   // Update is called once per frame
    void Update()
    {
        UpdateSpots();
    }

    public void UpdateSpots()
    {
        
        foreach(KeyValuePair<GameObject,InventorySpot> m_spot in itemsDisplayed)
        {
            if (m_spot.Value.ID >= 0)
            {
                m_spot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.GetItem[m_spot.Value.item.Id].ItemIcon;
                m_spot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                m_spot.Key.GetComponentInChildren<TextMeshProUGUI>().text = m_spot.Value.amount == 1 ? "" : m_spot.Value.amount.ToString("n0");

            }
            else
            {
                m_spot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                //Changing Alpha to 0 
                m_spot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                m_spot.Key.GetComponentInChildren<TextMeshProUGUI>().text =  "";
            }
        }
    }

    //Displays each item icon on the screen
    public void CreateDisplaySpot()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySpot>();
        for(int i = 0;i<inventory.Container.ObjectItems.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            obj.GetComponent<Button>().onClick.AddListener(delegate { OnButtonClick(); });
            itemsDisplayed.Add(obj, inventory.Container.ObjectItems[i]);
        }
    }

    public void Dosmth()
    {
        Debug.Log("Did something");
    }

    //Calculates the variables of the distance between each icon
    public Vector3 GetPosition(int i)
    {
        return new Vector3(startX_item +(spaceBetwwenX * (i % columnNumber)),startY_item + (-spaceBetweenY * (i / columnNumber)), 0f);
    }

    public class MouseItem
    {
        public GameObject obj;
        public InventorySpot item;
        public InventorySpot hoverItem;
        public GameObject hoverObj;

    }

    public void OnDragStart(GameObject obj) {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        // The sprite is the same size as the item in inventory
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if(itemsDisplayed[obj].ID >= 0) {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.dataBase.GetItem[itemsDisplayed[obj].ID].ItemIcon;
            img.raycastTarget = false;
        }
    }

    private void ShowSmth()
    {
        Debug.Log(" Item pressed");
    }

   public void OnButtonClick()
    {



        Debug.Log(" Item pressed");



    }

}
