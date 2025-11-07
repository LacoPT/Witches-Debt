
public class SpeedUpMod : SpellMod
{
    public override void Apply(Spell spell)
    {
        spell.data.speed *= 1.5f;
    }
}