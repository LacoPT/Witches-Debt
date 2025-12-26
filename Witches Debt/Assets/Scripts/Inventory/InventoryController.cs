using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class InventoryController : MonoBehaviour, IActor
{
    private InventoryModel inventoryModel;
    private static InventoryController instance;
    public static InventoryController GetInstance => instance;

    [Header("UI components")] 
    [SerializeField] private GameObject inventory;
    [SerializeField] private InventoryItemsAssetManager inventoryItemsAssetManager;
    
    [SerializeField] private GameObject modsStorage;
    [SerializeField] private GameObject spellSlotPrefab;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject modsInventoryPrefab;
    // Временное решение с сериализацией модели
    [Header("ModelComponents")] 
    [SerializeField] List<SpellType> spells;
    [SerializeField] private int storageCapacity;
    [SerializeField] private int spellModsCapacity;
    private Dictionary<GameObject, SpellType> spellSlots;
    // Contains SpellSlot from item was dragged;
    private SpellSlot spellSlotFrom;
    public Dictionary<GameObject, SpellType> GetSpellSlots() => spellSlots;
    public void SetSpellFrom(SpellSlot spellSlot) => spellSlotFrom = spellSlot; 
    
    public void Initialize(IInstanceModel model)
    {
        inventoryModel = model as InventoryModel;
        instance = this;
        // Временное решение, пока нету некого подобия GameManager
        //inventoryModel = new InventoryModel(spells, storageCapacity, spellModsCapacity);
        spellSlots = new Dictionary<GameObject, SpellType>();
        
        var inventoryItemsConfigs = GetConfigsFromNames(inventoryModel.Storage);
        
        CreateInventorySlots(inventoryItemsConfigs, inventory, inventoryModel.StorageCapacity);
        foreach (var spell in inventoryModel.Spells)
        {
            var spellInventory = Instantiate(modsInventoryPrefab, modsStorage.transform);
            spellSlots.Add(spellInventory, spell);

            var inventoryItemsConfigsForSpell = GetConfigsFromNames(inventoryModel.SpellsStorages[spell]);
            
            CreateInventorySlots(inventoryItemsConfigsForSpell, spellInventory,
                inventoryModel.SpellModsCapacity);
        }
        
        // Кнопка удаления первого модификатора
        // removeButton.onClick.AddListener(RemoveFirstModificator);
        inventoryModel.OnInventoryChanged += UpdateInventoryView;
    }
    
    private List<InventoryItemConfig> GetConfigsFromNames(List<string> names)
    {
        var inventoryItemConfigs = new List<InventoryItemConfig>();
        
        foreach (var n in names)
        {
            if (n == null)
            {
                inventoryItemConfigs.Add(null);
                continue;
            }
            inventoryItemConfigs.Add(GetConfigFromName(n));
        }
        
        return inventoryItemConfigs;
    }

    private InventoryItemConfig GetConfigFromName(string inventoryConfigName)
    {
        var address = "InventoryItemsConfigs/" + inventoryConfigName;
        var itemConfig = inventoryItemsAssetManager.GetItemConfig(address);
        return itemConfig;
    }

    private void CreateInventorySlots(List<InventoryItemConfig> items, GameObject inventoryToAdd, int capacity)
    {
        for (var i = 0; i < capacity; i++)
        {
            var spellSlot = Instantiate(spellSlotPrefab, inventoryToAdd.transform);
            spellSlot.GetComponent<SpellSlot>().SetIndex(i);
            if (items[i] != null)
            {
                var spellModGo = Instantiate(inventoryItemPrefab, spellSlot.transform);
                spellModGo.GetComponent<InventoryItemUI>().InitializeItem(items[i]);
            }
        }
    }

    private void UpdateInventoryView()
    {
        UpdateInventorySlotsView(inventoryModel.Storage, inventory, inventoryModel.StorageCapacity);
        foreach (var spell in inventoryModel.Spells)
        {
            if (spellSlots.ContainsValue(spell))
            {
                var spellInventory = spellSlots.FirstOrDefault(x => x.Value == spell).Key;
                UpdateInventorySlotsView(inventoryModel.SpellsStorages[spell], spellInventory,
                    inventoryModel.SpellModsCapacity);
            }
            else
            {
                var spellInventory = Instantiate(modsInventoryPrefab, modsStorage.transform);
                spellSlots.Add(spellInventory, spell);
                CreateInventorySlots(GetConfigsFromNames(inventoryModel.SpellsStorages[spell]), spellInventory, inventoryModel.SpellModsCapacity);;
            }
        }
    }

    //Test Method
    private void RemoveFirstModificator() => inventoryModel.RemoveItemFromStorage(0);
    
    private void UpdateInventorySlotsView(List<string> items, GameObject inventoryToAdd, int capacity)
    {
        for (var i = 0; i < capacity; i++)
        {
            var spellSlot = inventoryToAdd.transform.GetChild(i).gameObject;
            if (items[i] == null)
            {
                if (spellSlot.transform.childCount > 0)
                    Destroy(spellSlot.transform.GetChild(0).GameObject());
                continue;
            }
            
            var itemConfig = GetConfigFromName(items[i]);
            if (spellSlot.transform.childCount > 0)
            {
                var inventoryItem = spellSlot.transform.GetChild(0).GetComponent<InventoryItemUI>();
                
                if (inventoryItem.Item.Name == itemConfig.Name)
                    continue;
                
                Destroy(spellSlot.transform.GetChild(0).gameObject);
                inventoryItem.InitializeItem(itemConfig);
            }
            var spellModGo = Instantiate(inventoryItemPrefab, spellSlot.transform);
            var inventoryItemUI = spellModGo.GetComponent<InventoryItemUI>();
            inventoryItemUI.InitializeItem(itemConfig);
        }
    }
    
    private void SpawnNewSpellMod(InventoryItemConfig spellMod, SpellSlot slot)
    {
        if (inventoryModel.TryAddItemToInventory(spellMod))
        {
            var newSpellModGo = Instantiate(inventoryItemPrefab, slot.transform);
            var inventorySpellMod = newSpellModGo.GetComponent<InventoryItemUI>();
            inventorySpellMod.InitializeItem(spellMod);
        }
    }

    public void ReplaceMods(SpellSlot slotTo)
    {
        var inventoryFrom = inventoryModel.Storage;
        var inventoryTo = inventoryModel.Storage;
        
        if (spellSlots.TryGetValue(spellSlotFrom.transform.parent.gameObject, out var spellTypeFrom))
            inventoryFrom = inventoryModel.SpellsStorages[spellTypeFrom];
        
        if (spellSlots.TryGetValue(slotTo.transform.parent.gameObject, out var spellTypeTo))
            inventoryTo = inventoryModel.SpellsStorages[spellTypeTo];
        
        inventoryModel.MoveItem(spellSlotFrom.index, slotTo.index, inventoryFrom, inventoryTo);
    }
    private List<String> GetNamesFromConfigs(List<InventoryItemConfig> configs)
    {
        var names = new List<string>();
        foreach (var config in configs)
        {
            if (config == null)
                names.Add(null);
            else
                names.Add(config.ToString());
        }
        return names;
    }
    
    private List<InventoryItemConfig> GetInventoryFromSpellSlot(SpellSlot spellSlot)
    {
        var parentInventory = spellSlot.transform.parent.GameObject();
        SpellType spellType;
        if (!spellSlots.ContainsKey(parentInventory)) 
            return GetConfigsFromNames(inventoryModel.Storage);
        spellType = spellSlots[parentInventory];
        return GetConfigsFromNames(inventoryModel.SpellsStorages[spellType]);
    }
}