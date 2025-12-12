using System.Collections.Generic;

public class InventorySaveData
{
    public List<InventoryItemConfig> Storage;
    public Dictionary<SpellPrefabConfig, List<InventoryItemConfig>> SpellsStorages;

    public InventorySaveData()
    {
    }


}