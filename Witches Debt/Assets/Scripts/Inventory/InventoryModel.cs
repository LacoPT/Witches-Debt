using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryModel
{
    public event Action OnInventoryChanged; 
    
    private List<string> storage;
    private Dictionary<SpellType, List<string>> spellsStorages;
    private int storageCapacity;
    private int spellModsCapacity;
    private static InventoryModel Instance;

    public static InventoryModel GetInstance() => Instance;
    public List<string> Storage => storage;
    public List<SpellType> Spells => spellsStorages.Keys.ToList();
    public Dictionary<SpellType, List<string>> SpellsStorages => spellsStorages;
    public int StorageCapacity => storageCapacity;
    public int SpellModsCapacity => spellModsCapacity;
    
    /// <summary> Create InventoryModel with empty slots</summary>
    public InventoryModel(
        List<SpellType> spells,
        int storageCapacity,
        int spellModsCapacity)
    {
        storage = new List<string>();
        spellsStorages = new Dictionary<SpellType, List<string>>();
        
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

    public bool TryAddNewSpell(SpellType spellType)
    {
        if (!spellsStorages.ContainsKey(spellType))
        {
            spellsStorages.Add(spellType, new List<string>());
            for (var i = 0; i < spellModsCapacity; i++)
                spellsStorages[spellType].Add(null);
            OnInventoryChanged?.Invoke();
            return true;
        }
        return false;
    }
    
    //TODO: переписать так, чтобы данный метод принимал строки
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
        List<string> inventoryFrom,
        List<string> inventoryTo)
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
    
    public enum SpellTypes
    {
        Shoot,
        Area
    }

    //public InventorySaveData ToSaveData()
    //{
    //    var data = new InventorySaveData();
    //    data.Storage = storage;
    //    data.SpellsStorages = spellsStorages;
    //    return data;
    //}
    
    //public void FromSaveData(InventorySaveData data)
    //{
    //    storage = data.Storage;
    //    spellsStorages = data.SpellsStorages;
    //}
}
