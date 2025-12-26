using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class SpellConfiguration
{
   //These are set externally instead of in constructor, because they're initalizing with container and i don't know
   //how to pass parameters to them
   public GameObject Prefab { get; set; }
   public SpellType Type { get; set; }
   public List<SpellMod> Mods { get; set; } = new();

   private ModLibrary library;

   [Inject]
   public void Construct(ModLibrary library)
   {
       this.library = library;
   }
   
   public Spell ApplyMods(Spell spell)
   {
      foreach (var mod in Mods) mod.Apply(spell);
      spell.config = this;
      return spell;
   }
}