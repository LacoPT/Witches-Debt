using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using RarityPools = System.Collections.Generic.Dictionary<ModRarity, System.Collections.Generic.List<SpellMod>>;

public class ModLibrary
{
    public readonly Dictionary<string, SpellMod> Mods = new();
    private readonly RarityPools rarityPools = new()
    {
        {ModRarity.Common, new()},
        {ModRarity.Rare, new()},
        {ModRarity.Natural, new()},
        {ModRarity.Chaotic, new()}
    };
    
    private ModRarityDistribution distribution;
    private DiContainer container;
    
    public ModLibrary(ModRarityDistribution distribution, DiContainer container)
    {
        this.distribution = distribution;
        this.container = container;
        RegisterMod(container.Instantiate<SpeedUpMod>());
        RegisterMod(container.Instantiate<RocketMod>());
        RegisterMod(container.Instantiate<TripleShot>());
        RegisterMod(container.Instantiate<PoisonMod>());
    }

    private void RegisterMod(SpellMod mod)
    {
        Mods.Add(mod.ToString(), mod);
        rarityPools[mod.Rarity].Add(mod);
    }

    public SpellMod GetCompletelyRandomMod()
    {
        int index = UnityEngine.Random.Range(0, Mods.Count);
        return Mods.Values.ElementAt(index);
    }

    public SpellMod GetRandomMod()
    {
        var rarity = ModRarity.Common;
        do
        {
            var key = UnityEngine.Random.Range(0, distribution.Sum + 1);
            rarity = distribution.GetRarity(key);
        } while (rarityPools[rarity].Count == 0);
        var rarityPool =  rarityPools[rarity];
        var index = UnityEngine.Random.Range(0, rarityPool.Count);
        return rarityPool[index];
    }

    public SpellMod GetModByName(string modName)
    {
        return Mods[modName];
    }

    public T Resolve<T>() where T : SpellMod
    {
        return Mods[typeof(T).Name] as T;
    }
}