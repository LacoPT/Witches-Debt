using System.Collections.Generic;

public class SpellConfigurationSaveData
{
    public SpellType Type { get; }
    public List<SpellMod> Mods { get; }

    public SpellConfigurationSaveData(SpellType type, List<SpellMod> mods)
    {
        Type = type;
        Mods = mods;
    }
}