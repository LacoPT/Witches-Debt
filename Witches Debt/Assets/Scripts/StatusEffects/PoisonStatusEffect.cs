public class PoisonStatusEffect : StatusEffect, IDamageEffect
{
    public override StatusEffectType StatusEffectType => StatusEffectType.Dot;
    
    public override void OnApply(EnemyHittable hittable, EnemyModelMB model, int stacks)
    {
    }
    public float DamagePerStack => 5;
    public float GetDamage(EnemyModelMB model, int stack)
    {
        return DamagePerStack * stack;
    }
}