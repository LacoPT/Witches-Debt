using System.Collections.Generic;

public class InventorySaveData
{
    public List<InventoryItemConfig> Storage { get; }
    public Dictionary<SpellType, List<InventoryItemConfig>> SpellsStorages { get; }

    public InventorySaveData(List<InventoryItemConfig> storage, 
                             Dictionary<SpellType, List<InventoryItemConfig>> spellsStorages)
    {
        Storage = storage;
        SpellsStorages = spellsStorages;
    }

}