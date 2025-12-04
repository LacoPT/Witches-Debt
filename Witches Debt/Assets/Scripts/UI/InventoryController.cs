using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private InventoryModel inventoryModel;
    [Header("UI components")]
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject modsStorage;
    [SerializeField] private GameObject spellSlotPrefab;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject modsInventoryPrefab;
    // Временное решение с добавлением спеллов
    [Header("ModelComponents")]
    [SerializeField] List<SpellType> spells;
    [SerializeField] private int storageCapacity;
    [SerializeField] private int spellModsCapacity;
    private Dictionary<SpellType, GameObject> spellSlots;

    public void Awake()
    { 
        // Временное решение, пока нету некого подобия GameManager
        inventoryModel = new InventoryModel(spells, storageCapacity, spellModsCapacity);
        spellSlots = new Dictionary<SpellType, GameObject>();

        for (var i = 0; i < inventoryModel.StorageCapacity(); i++)
        {
            var spellSlot = Instantiate(spellSlotPrefab, inventory.transform);
            if (inventoryModel.Storage()[i] != null)
            {
                var spellModGo = Instantiate(inventoryItemPrefab, spellSlot.transform);
                spellModGo.GetComponent<InventoryItem>().InitializeItem(inventoryModel.Storage()[i]);
            }
        }

        foreach (var spell in spells)
        {
            spellSlots.Add(spell, new GameObject());
            var spellInventory = Instantiate(modsInventoryPrefab, modsStorage.transform);
            for (var i = 0; i < inventoryModel.SpellModsCapacity(); i++)
            {
                var spellSlot = Instantiate(spellSlotPrefab, spellInventory.transform);
                if (inventoryModel.Storage()[i] != null)
                {
                    var spellModGo = Instantiate(inventoryItemPrefab, spellSlot.transform);
                    spellModGo.GetComponent<InventoryItem>().InitializeItem(inventoryModel.SpellsStorages()[spell][i]);
                }
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
