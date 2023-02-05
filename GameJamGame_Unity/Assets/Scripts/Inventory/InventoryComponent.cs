using System.Collections.Generic;
using UnityEngine;

public delegate void OnInventoryChangedDelegate(string itemId, Sprite itemSprite, bool isAdded);
public delegate void OnFailedToUseInventoryDelegate(string failMessage);

public class InventoryComponent : MonoBehaviour, IHUDInteractor
{
    private const int maxSlots = 5;

    [Space(10)]
    [SerializeField]
    private InventoryWindow inventoryWindowPrefab;
    [SerializeField]
    private string missingItemMessage = "You do not have the required item!";
    [SerializeField]
    private string inventoryFullMessage = "The inventory is full!";

    private Dictionary<string /*index*/, IInventoryItem> items = new Dictionary<string, IInventoryItem>();

    public OnInventoryChangedDelegate OnInventoryChanged;
    public OnFailedToUseInventoryDelegate OnFailedToUseInventory;

    private void Start()
    {
        World.Instance.GetHUD().AddSubWindow(inventoryWindowPrefab, this);
    }

    public MonoBehaviour GetOwner()
    {
        return this;
    }

    public void TryUseItem(string itemId)
    {
        if(items.TryGetValue(itemId, out IInventoryItem item))
        {
            bool shouldDestroyItem = false;
            item.UseItem(out shouldDestroyItem);

            if(shouldDestroyItem)
            {
                items.Remove(itemId);
                OnInventoryChanged?.Invoke(itemId, item.GetSprite(), false);
            }
        }
        else
        {
            OnFailedToUseInventory?.Invoke(missingItemMessage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the inventory is full there's no need to do the collision check
        if (items.Count < maxSlots)
        {
            IInventoryItem item = collision.GetComponent<IInventoryItem>();

            if (item != null)
            {
                if (item.TryPickup())
                {
                    string itemId = item.GetItemID();
                    items.Add(itemId, item);
                    OnInventoryChanged?.Invoke(itemId, item.GetSprite(), true);
                }
            }
        }
        else
        {
            OnFailedToUseInventory?.Invoke(inventoryFullMessage);
        }
    }
}
