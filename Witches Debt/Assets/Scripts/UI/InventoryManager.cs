using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    [FormerlySerializedAs("spellSlots")] [SerializeField] private SpellSlot[] spellModsSlots;
    [SerializeField] private GameObject inventoryItemPrefab;
    //<summary>
    // Adds item into inventory
    // If success returns true,
    // If filed returns false
    //</summary>
    public bool AddSpellModificator(InventoryItemSO spellMod)
    {
        for (var i = 0; i < spellModsSlots.Length; i++)
        {
            var slot = spellModsSlots[i];
            var modInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (modInSlot == null)
            {
                SpawnNewSpellMod(spellMod, slot);
                return true;
            }
        }
        return false;
    }
    public void SpawnNewSpellMod(InventoryItemSO spellMod, SpellSlot slot)
    {
        var newSpellModGo = Instantiate(inventoryItemPrefab, slot.transform);
        var inventorySpellMod = newSpellModGo.GetComponent<InventoryItem>();
        inventorySpellMod.InitializeItem(spellMod);
    }

}
