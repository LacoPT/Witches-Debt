using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryModel
{
    private List<InventoryItemConfig> storage;
    private Dictionary<SpellType, List<InventoryItemConfig>> spellsStorages;
    private int storageCapacity;
    private int spellModsCapasity;

    public List<InventoryItemConfig> Storage() => storage;
    public List<SpellType> Spells() => spellsStorages.Keys.ToList();
    public List<InventoryItemConfig> SpellStorage(SpellType spellType) => spellsStorages[spellType];
    public Dictionary<SpellType, List<InventoryItemConfig>> SpellsStorages() => spellsStorages;
    public int StorageCapacity() => storageCapacity;
    public int SpellModsCapacity() => spellModsCapasity;

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
