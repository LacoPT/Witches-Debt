using System;
using System.Collections.Generic;
using System.Linq;

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
    
    public ModLibrary(ModRarityDistribution distribution)
    {
        this.distribution = distribution;
        RegisterMod(new SpeedUpMod());
        RegisterMod(new RocketMod());
        RegisterMod(new TripleShot());
    }

    public void RegisterMod(SpellMod mod)
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
}