using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class SpellConfiguration
{
   public SpellType type;
   public List<SpellMod> mods = new();

   private ModLibrary library;

   [Inject]
   public void Construct(ModLibrary library)
   {
       this.library = library;
   }
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
            mods.Add(library.GetModByName(mod));
        }
    }
}