using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryModel
{
    public event Action OnInventoryChanged; 
    
    private List<string> storage;
    private Dictionary<SpellPrefabConfig, List<string>> spellsStorages;
    private int storageCapacity;
    private int spellModsCapacity;
    private static InventoryModel instance;

    public static InventoryModel GetInstance() => instance;
    public List<string> Storage => storage;
    public List<SpellPrefabConfig> Spells => spellsStorages.Keys.ToList();
    public List<string> SpellStorage(SpellPrefabConfig spellPrefabConfig) => spellsStorages[spellPrefabConfig];
    public Dictionary<SpellPrefabConfig, List<string>> SpellsStorages => spellsStorages;
    public int StorageCapacity => storageCapacity;
    public int SpellModsCapacity => spellModsCapacity;

    /// <summary> Create InventoryModel with empty slots</summary>
    public InventoryModel(
        List<SpellPrefabConfig> spells,
        int storageCapacity,
        int spellModsCapacity)
    {
        storage = new List<string>();
        spellsStorages = new Dictionary<SpellPrefabConfig, List<string>>();
        
        for (var i = 0; i < storageCapacity; i++)
            storage.Add(null);

        foreach (var spell in spells)
        {
            spellsStorages[spell] = new List<string>();
            for (var i = 0; i < spellModsCapacity; i++)
                spellsStorages[spell].Add(null);
        }
        this.storageCapacity = storageCapacity;
        this.spellModsCapacity = spellModsCapacity;
    }

    public bool TryAddNewSpell(SpellPrefabConfig spellPrefabConfig)
    {
        if (!spellsStorages.ContainsKey(spellPrefabConfig))
        {
            spellsStorages.Add(spellPrefabConfig, new List<string>());
            for (var i = 0; i < spellModsCapacity; i++)
                spellsStorages[spellPrefabConfig].Add(null);
            OnInventoryChanged?.Invoke();
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
                storage[i] = item.ToString();
                OnInventoryChanged?.Invoke();
                return true;
            }
        }
        return false;
    }

    public void MoveItem(
        int slotFrom, 
        int slotTo, 
        List<InventoryItemConfig> inventoryFrom,
        List<InventoryItemConfig> inventoryTo)
    {
        (inventoryFrom[slotFrom], inventoryTo[slotTo]) = (inventoryTo[slotTo], inventoryFrom[slotFrom]);
        OnInventoryChanged?.Invoke();
    }

    // TODO: реализовать после того, как продумаю систему удаления модификаторов
    public void RemoveItemFromStorage(int index)
    {
        storage[index] = null;
        OnInventoryChanged?.Invoke();
    }

    public InventorySaveData ToSaveData()
    {
        var data = new InventorySaveData();
        data.Storage = storage;
        data.SpellsStorages = spellsStorages;
        return data;
    }

    public void FromSaveData(InventorySaveData data)
    {
        storage = data.Storage;
        spellsStorages = data.SpellsStorages;
    }
}
