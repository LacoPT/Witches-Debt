using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryModel
{
    public event Action OnInventoryChanged; 
    
    private List<InventoryItemConfig> storage;
    private Dictionary<SpellType, List<InventoryItemConfig>> spellsStorages;
    private int storageCapacity;
    private int spellModsCapacity;

    public List<InventoryItemConfig> Storage() => storage;
    public List<SpellType> Spells() => spellsStorages.Keys.ToList();
    public List<InventoryItemConfig> SpellStorage(SpellType spellType) => spellsStorages[spellType];
    public Dictionary<SpellType, List<InventoryItemConfig>> SpellsStorages() => spellsStorages;
    public int StorageCapacity() => storageCapacity;
    public int SpellModsCapacity() => spellModsCapacity;
    /// <summary> Create InventoryModel with empty slots</summary>
    public InventoryModel(
        List<SpellType> spells,
        int storageCapacity,
        int spellModsCapacity)
    {
        storage = new List<InventoryItemConfig>();
        spellsStorages = new Dictionary<SpellType, List<InventoryItemConfig>>();
        
        for (var i = 0; i < storageCapacity; i++)
            storage.Add(null);

        foreach (var spell in spells)
        {
            spellsStorages[spell] = new List<InventoryItemConfig>();
            for (var i = 0; i < spellModsCapacity; i++)
                spellsStorages[spell].Add(null);
        }
        this.storageCapacity = storageCapacity;
        this.spellModsCapacity = spellModsCapacity;
    }

    /// <summary> Create InventoryModel with current inventory storage and spells storages</summary>
    public InventoryModel(
        List<InventoryItemConfig> storage,
        Dictionary<SpellType, List<InventoryItemConfig>> spellsStorages 
        )
    {
        this.storage = storage;
        this.spellsStorages = spellsStorages;
        storageCapacity = storage.Count;
        spellModsCapacity = spellsStorages.Values.ToList()[0].Count;
    }

    public bool TryAddNewSpell(SpellType spellType)
    {
        if (!spellsStorages.ContainsKey(spellType))
        {
            spellsStorages.Add(spellType, new List<InventoryItemConfig>());
            for (var i = 0; i < spellModsCapacity; i++)
                spellsStorages[spellType].Add(null);
            return true;
        }
        return false;
    }
    
    public bool TryAddItemToInventory(InventoryItemConfig item)
    {
        for (var i = 0; i < storageCapacity; i++)
        {
            if (storage[i] == null)
            {
                storage[i] = item;
                return true;
            }
            OnInventoryChanged?.Invoke();
        }
        return false;
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
        OnInventoryChanged?.Invoke();
    }

    // TODO: реализовать после того, как продумаю систему удаления модификаторов
    public void RemoveItemFromInventory(InventoryItemConfig item, List<InventoryItemConfig> inventoryRemoveFrom)
    {
    }
    

}
