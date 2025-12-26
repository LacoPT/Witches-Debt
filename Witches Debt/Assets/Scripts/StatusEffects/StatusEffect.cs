public abstract class StatusEffect
{
    public abstract StatusEffectType StatusEffectType { get; }
    public abstract void OnApply(EnemyHittable hittable, EnemyModelMB model, int stacks);
}