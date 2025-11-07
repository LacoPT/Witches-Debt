using System.Collections.Generic;
using UnityEngine;

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
}