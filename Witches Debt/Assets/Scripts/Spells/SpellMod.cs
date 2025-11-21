public abstract class SpellMod
{ 
    public abstract ModRarity Rarity { get; }
    public abstract void Apply(Spell spell);
}