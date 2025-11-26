
public class SpeedUpMod : SpellMod
{
    public override ModRarity Rarity => ModRarity.Common;
    
    public override void Apply(Spell spell)
    {
        spell.Data.speed *= 1.5f;
    }
}