public interface IDamageEffect
{
    public float DamagePerStack { get; }
    public float GetDamage(EnemyModelMB model, int stack);
}