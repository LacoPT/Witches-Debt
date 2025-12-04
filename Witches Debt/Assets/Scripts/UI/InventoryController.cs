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
            spellSlots.Add(spell, new GameObject());
            var spellInventory = Instantiate(modsInventoryPrefab, modsStorage.transform);
            CreateInventorySlots(inventoryModel.SpellsStorages()[spell], spellInventory,
                inventoryModel.SpellModsCapacity());
        }

        removeButton.onClick.AddListener(RemoveFirstModificator);
        inventoryModel.OnInventoryChanged += UpdateInventoryView;
        var result = inventoryModel.TryAddItemToInventory(configToAdd);
    }

    private void UpdateInventoryView()
    {
        ReplaceInventorySlots(inventoryModel.Storage(), inventory, inventoryModel.StorageCapacity());
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
                var inventoryItem = spellSlot.transform.GetChild(0).GetComponent<InventoryItem>();
                if (inventoryItem.Item == items[i])
                    continue;
                inventoryItem.InitializeItem(items[i]);
            }
            var spellModGo = Instantiate(inventoryItemPrefab, spellSlot.transform);
            spellModGo.GetComponent<InventoryItem>().InitializeItem(items[i]);
        }
    }

    private void CreateInventorySlots(List<InventoryItemConfig> items, GameObject inventoryToAdd, int capacity)
    {
        for (var i = 0; i < capacity; i++)
        {
            var spellSlot = Instantiate(spellSlotPrefab, inventoryToAdd.transform);
            if (items[i] != null)
            {
                var spellModGo = Instantiate(inventoryItemPrefab, spellSlot.transform);
                spellModGo.GetComponent<InventoryItem>().InitializeItem(items[i]);
            }
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
            var inventorySpellMod = newSpellModGo.GetComponent<InventoryItem>();
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