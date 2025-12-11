using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    // Временное решеине для введения модели
    [FormerlySerializedAs("spellSlots")] [SerializeField] private SpellSlot[] spellModsSlots;
    [SerializeField] private GameObject inventoryItemPrefab;
    /// <summary>Adds item into inventory.</summary>
    /// <returns> returns True if added with success, False if adding is filed (slots are full for example). </returns>
    /// <param name="spellMod"> Scriptable Object of Inventory Spell Modificator. </param>
    public bool AddSpellModificator(InventoryItemConfig spellMod)
    {
        foreach (var slot in spellModsSlots)
        {
            var modInSlot = slot.GetComponentInChildren<InventoryItemUI>();
            if (modInSlot != null) continue;
            SpawnNewSpellMod(spellMod, slot);
            return true;
        }
        
        return false;
    }
    private void SpawnNewSpellMod(InventoryItemConfig spellMod, SpellSlot slot)
    {
        var newSpellModGo = Instantiate(inventoryItemPrefab, slot.transform);
        var inventorySpellMod = newSpellModGo.GetComponent<InventoryItemUI>();
        inventorySpellMod.InitializeItem(spellMod);
    }

}
