using System.Collections.Generic;

public class StatusEffectLibrary
{
    public readonly Dictionary<string, StatusEffect> StatusEffects = new();

    public StatusEffectLibrary()
    {
        RegisterStatusEffect(new PoisonStatusEffect());
    }

    public T Resolve<T>() where T : StatusEffect
    {
        return StatusEffects[typeof(T).Name] as T;
    }

    public StatusEffect GetStatusEffectByString(string statusEffect)
    {
        return StatusEffects[statusEffect];
    }

    private void RegisterStatusEffect(StatusEffect statusEffect)
    {
        StatusEffects[statusEffect.ToString()] = statusEffect;
    }
}