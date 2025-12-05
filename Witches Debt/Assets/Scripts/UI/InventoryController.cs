using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class InventoryController : MonoBehaviour
{
    private InventoryModel inventoryModel;

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
    private Dictionary<SpellType, GameObject> spellSlots;

    public void Awake()
    {
        // Временное решение, пока нету некого подобия GameManager
        inventoryModel = new InventoryModel(spells, storageCapacity, spellModsCapacity);
        spellSlots = new Dictionary<SpellType, GameObject>();

        CreateInventorySlots(inventoryModel.Storage(), inventory, inventoryModel.StorageCapacity());
        foreach (var spell in inventoryModel.Spells())
        {
            var spellInventory = Instantiate(modsInventoryPrefab, modsStorage.transform);
            spellSlots.Add(spell, spellInventory);
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
        ReplaceInventorySlots(inventoryModel.Storage(), inventory, inventoryModel.StorageCapacity());
        foreach (var spell in inventoryModel.Spells())
        {
            if (spellSlots.ContainsKey(spell))
                ReplaceInventorySlots(inventoryModel.SpellsStorages()[spell], spellSlots[spell], inventoryModel.SpellModsCapacity());
            else
            {
                var spellInventory = Instantiate(modsInventoryPrefab, modsStorage.transform);
                spellSlots.Add(spell, spellInventory);
                CreateInventorySlots(inventoryModel.SpellsStorages()[spell], spellInventory, inventoryModel.SpellModsCapacity());;
            }
        }
    }

    //Test Method
    public void RemoveFirstModificator() => inventoryModel.RemoveItemFromStorage(0);

    
    private void ReplaceInventorySlots(List<InventoryItemConfig> items, GameObject inventoryToAdd, int capacity)
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}