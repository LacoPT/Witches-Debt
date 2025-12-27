using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryModel : IInstanceModel
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


    // TODO: replace with scriptable object
    private const int DEFAULT_STORAGE_CAPACITY = 6;
    private const int DEFAULT_SPELL_MODS_CAPACITY = 4;
    private readonly List<SpellType> DEFAULT_SPELLS = new() { SpellType.Shot };

    public InventoryModel()
    {
        storage = new List<string>();
        spellsStorages = new Dictionary<SpellType, List<string>>();
        storageCapacity = DEFAULT_STORAGE_CAPACITY;
        spellModsCapacity = DEFAULT_SPELL_MODS_CAPACITY;

        for (var i = 0; i < storageCapacity; i++)
            storage.Add(null);
        
        foreach (var spell in DEFAULT_SPELLS)
        {
            //spellsStorages[spell] = new List<string>();
            spellsStorages[spell] = new() { new RocketMod().ToString(), new TripleShot().ToString() }; // temporary solution for testing purposes TODO: remove

            for (var i = 0; i < spellModsCapacity - spellsStorages.Count; i++)
                spellsStorages[spell].Add(null);
        }
        
        // temporary solution for adding mods into inventory
        storage[0] = nameof(RocketMod);
        storage[1] = nameof(TripleShot);
        storage[2] = nameof(SpeedUpMod);
        storage[3] = nameof(PoisonMod);
        
        
        Instance = this;
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

    public InventorySaveData ToSaveData()
    {
        var saveStorages = new List<SpellEntry>();
        foreach (var kvPair in spellsStorages)
        {
            saveStorages.Add(new SpellEntry(kvPair.Key, kvPair.Value));
        }
        var data = new InventorySaveData
        {
            Storage = storage,
            SpellsStorages = saveStorages
        };
        return data;
    }

    public void FromSaveData(InventorySaveData data)
    {
        storage = data.Storage;
        foreach (var entry in data.SpellsStorages)
        {
            spellsStorages[entry.type] = entry.mods;
        }
    }
}
