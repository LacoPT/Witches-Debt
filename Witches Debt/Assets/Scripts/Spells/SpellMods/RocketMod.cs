using UnityEngine;

public class RocketMod : SpellMod
{
    private const float SPEED_UP_FACTOR = 15f;
    
    public override ModRarity Rarity => ModRarity.Common;

    public override void Apply(Spell spell)
    {
        spell.data.speed *= 0.1f;
        spell.AfterUpdate.AddListener(() =>
        {
            spell.data.speed += SPEED_UP_FACTOR * Time.deltaTime;
        });
    }
}
