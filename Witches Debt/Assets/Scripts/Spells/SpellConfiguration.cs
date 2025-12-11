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
        foreach (var mod in mods)
        {
            data.ModTypes.Add(mod.GetType().Name);
        }
        return data;
    }

    public void FromSaveData(SpellConfigurationSaveData data)
    {
        type = data.Type;
        foreach (var mod in data.ModTypes)
        {
            mods.Add(GetModByString(mod));
        }
    }

    // Temporary solution for testing purposes
    public SpellMod GetModByString(string name)
    {
        switch (name)
        {
            case "TripleShot":
            {
                return new TripleShot();
            }
            case "RocketMod":
            {
                return new RocketMod();
            }
            default:
            {
                return new SpeedUpMod();
            }
        }
    }
}