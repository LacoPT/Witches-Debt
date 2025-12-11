using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class InventoryController : MonoBehaviour
{
    private InventoryModel inventoryModel;
    private static InventoryController instance;
    public static InventoryController GetInstance() => instance;

    [Header("UI components")] [SerializeField]
    private GameObject inventory;

    [SerializeField] private GameObject modsStorage;
    [SerializeField] private GameObject spellSlotPrefab;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject modsInventoryPrefab;
    // Тестовые поля
    [SerializeField] private Button removeButton;
    [SerializeField] private InventoryItemConfig configToAdd;
    // Временное решение с сериализацией модели
    [Header("ModelComponents")] [SerializeField]
    List<SpellType> spells;

    [SerializeField] private int storageCapacity;
    [SerializeField] private int spellModsCapacity;
    
    private Dictionary<GameObject, SpellType> spellSlots;
    // Contains SpellSlot from item was dragged;
    private SpellSlot spellSlotFrom;
    public Dictionary<GameObject, SpellType> GetSpellSlots() => spellSlots;

    public void Awake()
    {
        instance = this;
        // Временное решение, пока нету некого подобия GameManager
        inventoryModel = new InventoryModel(spells, storageCapacity, spellModsCapacity);
        spellSlots = new Dictionary<GameObject, SpellType>();

        CreateInventorySlots(inventoryModel.Storage(), inventory, inventoryModel.StorageCapacity());
        foreach (var spell in inventoryModel.Spells())
        {
            var spellInventory = Instantiate(modsInventoryPrefab, modsStorage.transform);
            spellSlots.Add(spellInventory, spell);
            CreateInventorySlots(inventoryModel.SpellsStorages()[spell], spellInventory,
                inventoryModel.SpellModsCapacity());
        }

        removeButton.onClick.AddListener(RemoveFirstModificator);
        inventoryModel.OnInventoryChanged += UpdateInventoryView;
        inventoryModel.TryAddItemToInventory(configToAdd);
        inventoryModel.TryAddItemToInventory(configToAdd);
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
        UpdateInventorySlotsView(inventoryModel.Storage(), inventory, inventoryModel.StorageCapacity());
        foreach (var spell in inventoryModel.Spells())
        {
            if (spellSlots.ContainsValue(spell))
            {
                var spellInventory = spellSlots.FirstOrDefault(x => x.Value == spell).Key;
                UpdateInventorySlotsView(inventoryModel.SpellsStorages()[spell], spellInventory,
                    inventoryModel.SpellModsCapacity());
            }
            else
            {
                var spellInventory = Instantiate(modsInventoryPrefab, modsStorage.transform);
                spellSlots.Add(spellInventory, spell);
                CreateInventorySlots(inventoryModel.SpellsStorages()[spell], spellInventory, inventoryModel.SpellModsCapacity());;
            }
        }
    }

    //Test Method
    public void RemoveFirstModificator() => inventoryModel.RemoveItemFromStorage(0);

    
    private void UpdateInventorySlotsView(List<InventoryItemConfig> items, GameObject inventoryToAdd, int capacity)
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

            if (spellSlot.transform.childCount > 0)
            {
                var inventoryItem = spellSlot.transform.GetChild(0).GetComponent<InventoryItemUI>();
                if (inventoryItem.Item == items[i])
                    continue;
                inventoryItem.InitializeItem(items[i]);
            }
            var spellModGo = Instantiate(inventoryItemPrefab, spellSlot.transform);
            spellModGo.GetComponent<InventoryItemUI>().InitializeItem(items[i]);
        }
    }
    
    private void SetNewSpellMod(InventoryItemConfig spellMod, int slotIndex)
    {
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
        var inventoryFrom = GetInventoryFromSpellSlot(spellSlotFrom);
        var inventoryTo = GetInventoryFromSpellSlot(slotTo);
        
        inventoryModel.MoveItem(spellSlotFrom.index, slotTo.index, inventoryFrom, inventoryTo);
    }
    
    public void SetSpellFrom(SpellSlot spellSlot) => spellSlotFrom = spellSlot; 
    
    private List<InventoryItemConfig> GetInventoryFromSpellSlot(SpellSlot spellSlot)
    {
        var parentInventory = spellSlot.transform.parent.GameObject();

        SpellType spellType = null;
        if (spellSlots.ContainsKey(parentInventory))
            spellType = spellSlots[parentInventory];
        
        if (spellType == null)
            return inventoryModel.Storage();
        return inventoryModel.SpellsStorages()[spellType];
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}