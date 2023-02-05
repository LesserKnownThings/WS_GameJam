
using System;
using UnityEngine;

public interface IInventoryItem
{
    //This is a bool so we can add additional checks for items if we ever need them
    bool TryPickup();
    void UseItem(out bool shouldDestroy);
    string GetItemID();
    Sprite GetSprite();
}
