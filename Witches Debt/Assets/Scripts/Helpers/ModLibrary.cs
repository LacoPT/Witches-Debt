using System;
using System.Collections.Generic;
using System.Linq;

public class ModLibrary
{
    public readonly Dictionary<string, SpellMod> Mods = new();

    public ModLibrary()
    {
        RegisterMod(new SpeedUpMod());
        RegisterMod(new RocketMod());
        RegisterMod(new TripleShot());
    }

    public void RegisterMod(SpellMod mod)
    {
        Mods.Add(mod.ToString(), mod);
    }

    public SpellMod GetRandomMod()
    {
        int index = UnityEngine.Random.Range(0, Mods.Count);
        return Mods.Values.ElementAt(index);
    }
}