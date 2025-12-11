using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpellConfiguration
{
   public SpellType type;
   public List<SpellMod> mods = new();

   public Spell ApplyMods(Spell spell)
   {
      foreach (var mod in mods) mod.Apply(spell);
      spell.config = this;
      return spell;
   }

    public SpellConfigurationSaveData ToSaveData()
    {
        var data = new SpellConfigurationSaveData();
        data.Type = type;
        data.Mods = mods;
        return data;
    }

    public void FromSaveData(SpellConfigurationSaveData data)
    {
        type = data.Type;
        mods = data.Mods;
    }
}