using System;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInventoryItem
{
    [SerializeField]
    private Sprite itemSprite = null;
    [SerializeField]
    private string itemID;
    [SerializeField]
    private bool shouldDestroyOnUse = false;

    private void Start()
    {
        // in case we forget to set an id or we don't need one
        if(string.IsNullOrEmpty(itemID))
        {
            itemID = Guid.NewGuid().ToString();
        }
    }

    public string GetItemID()
    {
        return itemID;
    }

    public Sprite GetSprite()
    {
        return itemSprite;
    }

    public bool TryPickup()
    {        
        gameObject.SetActive(false);
        return true;
    }

    public void UseItem(out bool shouldDestroy)
    {
        shouldDestroy = shouldDestroyOnUse;

        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
    }
}