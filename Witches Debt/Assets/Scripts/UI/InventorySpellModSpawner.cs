using UnityEngine;

public class InventorySpellModSpawner : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private ModRarityDistribution rarityDistribution;

    public void SpawnRandomSpellMod()
    {
        var modLibrary = new ModLibrary(rarityDistribution);
        var mod = modLibrary.GetRandomMod();
        var inventorySpellMod = (InventoryItemSO)Resources.Load("Assets/ScriptableObjects/InventoryItems/" + mod.GetType() + ".asset");
        inventoryManager.AddSpellModificator(inventorySpellMod);
    }
}
