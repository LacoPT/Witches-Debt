using System.Collections.Generic;

public class InventorySaveData
{
    private List<InventoryItemConfig> storage;
    private Dictionary<SpellType, List<InventoryItemConfig>> spellsStorages;

    public List<InventoryItemConfig> Storage => storage;
    public Dictionary<SpellType, List<InventoryItemConfig>> SpellStorages => spellsStorages;

    public InventorySaveData(List<InventoryItemConfig> storage, Dictionary<SpellType, 
                             List<InventoryItemConfig>> spellsStorages)
    {
        this.storage = storage;
        this.spellsStorages = spellsStorages;
    }
}