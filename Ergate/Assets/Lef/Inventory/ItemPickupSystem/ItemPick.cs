using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;


public class ItemPick : MonoBehaviour
{
  [field: SerializeField] public Item_s InventoryItem { get; private set; }

    [field: SerializeField] public int Quantity { get; set; } = 1;

    //[SerializeField] private AudioSource audioSource;

    [SerializeField] private float duration = 0.3f;

    private void Start()
    {
        try
        {
            GetComponent<ItemPick>().InventoryItem.ItemImage = InventoryItem.ItemImage;
        }
        catch
        {
            
        }
    }

    internal void DestroyItem()
    {
        
        GetComponent<Collider>().enabled = false;
        StartCoroutine(PickupEffect());
    }

    private IEnumerator PickupEffect()
    {
        //audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        transform.localScale = endScale;
        Destroy(gameObject);
    }
}
