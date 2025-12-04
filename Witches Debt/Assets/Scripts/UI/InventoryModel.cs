using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryModel
{
    private List<InventoryItemConfig> storage;
    private Dictionary<SpellType, List<InventoryItemConfig>> spells;
    private int storageCapacity;
    private int spellModsCapasity;

    public bool TryAddItemToInventory(InventoryItemConfig item)
    {
        if (storage.Count >= storageCapacity) return false;
        storage.Add(item);
        return true;
    }

    public void MoveItem(
        InventoryItemConfig itemToMove, 
        int slotFrom, 
        int slotTo, 
        List<InventoryItemConfig> inventoryFrom,
        List<InventoryItemConfig> inventoryTo)
    {
        inventoryFrom[slotFrom] = inventoryTo[slotTo];
        inventoryTo[slotTo] = itemToMove;
    }

    // TODO: реализовать после того, как продумаем систему удаления модификаторов
    public void RemoveItemFromInventory(InventoryItemConfig item, List<InventoryItemConfig> inventoryRemoveFrom)
    {
    }
    

}
