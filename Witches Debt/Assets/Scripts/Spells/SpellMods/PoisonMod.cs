using UnityEngine;

public class PoisonMod : SpellMod
{
    public override ModRarity Rarity => ModRarity.Natural;
    public override void Apply(Spell spell)
    {
        spell.Hit.AddListener((hittable) =>
        {
            hittable.ApplyEffect(statusEffectLibrary.Resolve<PoisonStatusEffect>());
        });
    }
}