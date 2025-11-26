using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "ModRarityDistribution", menuName = "Scriptable Objects/ModRarityDistribution")]
public class ModRarityDistribution : ScriptableObject
{
    [SerializeField]
    [Tooltip("This number corresponds of how many of given rarity entries are in pool | Bigger = more common")]
    private int common;
    [SerializeField]
    [Tooltip("This number corresponds of how many of given rarity entries are in pool | Bigger = more common")]
    private int rare;
    [SerializeField]
    [Tooltip("This number corresponds of how many of given rarity entries are in pool | Bigger = more common")]
    private int natural;
    [SerializeField]
    [Tooltip("This number corresponds of how many of given rarity entries are in pool | Bigger = more common")]
    private int chaotic;

    public int Common => common;
    public int Rare => rare;
    public int Natural => natural;
    public int Chaotic => chaotic;
    
    public int Sum => Common + Rare + Natural + Chaotic;
    public const int CommonKeyRequirements = 0;
    public int RareKeyRequirements => rare;
    public int NaturalKeyRequirements => RareKeyRequirements + natural;
    public int ChaoticKeyRequirements => NaturalKeyRequirements + chaotic;

    public ModRarity GetRarity(int key)
    {
        if(key < 0 || key > Sum)
            throw new System.ArgumentOutOfRangeException("key", key, "Key out of range.");
        if (key >= RareKeyRequirements)
            return ModRarity.Common;
        if (key >= NaturalKeyRequirements)
            return ModRarity.Rare;
        if (key >= ChaoticKeyRequirements)
            return ModRarity.Natural;
        return ModRarity.Chaotic;
    }
}
