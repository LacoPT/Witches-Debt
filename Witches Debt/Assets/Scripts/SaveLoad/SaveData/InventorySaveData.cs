using System.Collections.Generic;
using System.Xml.Serialization;

public class InventorySaveData
{
    public List<string> Storage;
    public List<SpellEntry> SpellsStorages;

    public InventorySaveData()
    {
    }
}