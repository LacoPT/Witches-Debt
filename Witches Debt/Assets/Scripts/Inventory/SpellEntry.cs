using NUnit.Framework;
using System.Collections.Generic;

public class SpellEntry
{
    public SpellType type;
    public List<string> mods;

    public SpellEntry()
    {
    }
    public SpellEntry(SpellType type, List<string> mods)
    {
        this.type = type;
        this.mods = mods;
    }
}