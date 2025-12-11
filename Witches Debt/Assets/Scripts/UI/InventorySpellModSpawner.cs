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
        var modToSpawn = modLibrary.GetRandomMod();
        Debug.Log(modToSpawn.GetType().Name);
        var inventorySpellMod = (InventoryItemConfig)Resources.Load("InventoryItems/" + modToSpawn.GetType().Name);
        inventoryManager.AddSpellModificator(inventorySpellMod);
    }
}
