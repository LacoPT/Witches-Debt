using System.Collections.Generic;

public class InventorySaveData
{
    public List<InventoryItemConfig> Storage;
    public Dictionary<SpellType, List<InventoryItemConfig>> SpellsStorages;

    public InventorySaveData()
    {
    }


}