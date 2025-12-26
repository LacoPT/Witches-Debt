using Zenject;

public abstract class SpellMod
{ 
    public abstract ModRarity Rarity { get; }
    public abstract void Apply(Spell spell);
    
    protected StatusEffectLibrary statusEffectLibrary;

    [Inject]
    public void Construct(StatusEffectLibrary statusEffectLibrary)
    {
        this.statusEffectLibrary = statusEffectLibrary;
    }
}