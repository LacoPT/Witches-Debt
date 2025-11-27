public abstract class StatusEffect
{
    public abstract void Apply(EnemyEffectHandler handler);
    public abstract void Tick(EnemyModelMB stacks);
}