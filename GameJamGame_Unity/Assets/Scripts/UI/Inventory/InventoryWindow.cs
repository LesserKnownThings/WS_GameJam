
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : HUDWindow
{
    private Dictionary<string /*ID*/, InventorySlot> inventorySlots = new Dictionary<string, InventorySlot>();
    private InventorySlot[] slots;

    private void Start()
    {
        slots = GetComponentsInChildren<InventorySlot>();
    }

    public override void InitHUDWindow(IHUDInteractor interactor)
    {
        base.InitHUDWindow(interactor);

        InventoryComponent inventoryComponent = (InventoryComponent)interactor;

        if (inventoryComponent != null)
        {
            inventoryComponent.OnFailedToUseInventory += OnFailedToUseInventory;
            inventoryComponent.OnInventoryChanged += OnInventoryChanged;
        }
    }

    private void OnFailedToUseInventory(string message)
    {
        if (World.Instance.GetUIManager().TryGetWindow(WindowActionType.Message, out SubWindow subWindow))
        {
            MessageWindow messageWindow = (MessageWindow)subWindow;

            if (messageWindow != null)
            {
                messageWindow.QueueMessage(message);
            }
        }
    }

    private void OnInventoryChanged(string itemId, Sprite itemSprite, bool isAdded)
    {
        if(isAdded)
        {
            if(TryGetEmptySlot(out InventorySlot slot))
            {
                slot.PopulateSlot(itemSprite);
                inventorySlots.Add(itemId, slot);
            }
        }
        else
        {
            if(inventorySlots.TryGetValue(itemId, out InventorySlot slot))
            {
                slot.ClearSlot();
                inventorySlots.Remove(itemId);
            }
        }
    }

    private bool TryGetEmptySlot(out InventorySlot slot)
    {
        int index = Array.FindIndex(slots, x => x.IsEmpty());
        if (index > -1)
        {
            slot = slots[index];
            return true;
        }

        slot = null;
        return false;
    }
}
