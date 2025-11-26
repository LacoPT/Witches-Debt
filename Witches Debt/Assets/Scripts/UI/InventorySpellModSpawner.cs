using System;
using UnityEngine;

public class InventorySpellModSpawner : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private ModRarityDistribution rarityDistribution;

    private ModLibrary modLibrary;
    public void Awake()
    {
        modLibrary = new ModLibrary(rarityDistribution);
    }
    
    public void SpawnRandomSpellMod()
    {
        var mod = modLibrary.GetRandomMod();
        Debug.Log(mod.GetType().Name);
        var inventorySpellMod = (InventoryItemSO)Resources.Load("InventoryItems/" + mod.GetType().Name);
        inventoryManager.AddSpellModificator(inventorySpellMod);
    }
}
