using UnityEngine;

public class RocketMod : SpellMod
{
    private const float SPEED_UP_FACTOR = 15f;
    
    public override ModRarity Rarity => ModRarity.Common;

    public override void Apply(Spell spell)
    {
        spell.Data.speed *= 0.1f;
        spell.afterUpdate.AddListener(() =>
        {
            spell.Data.speed += SPEED_UP_FACTOR * Time.deltaTime;
        });
    }
}
