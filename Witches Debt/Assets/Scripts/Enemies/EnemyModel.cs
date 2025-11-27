using System;

public class EnemyModel
{
    private EnemyNames name;
    private int cost;
    private float maxHealth;
    private float currentHealth;
    private float baseMovingSpeed;
    private float currentMovingSpeed;
    private float contactDamage;
    private float attackDamage;
    private float baseAttackSpeed;
    private float currentAttackSpeed;
    private PlayerTargetProvider target;

    public EnemyNames Name => name;
    public int Cost => cost;
    public float CurrentHealth => currentHealth;
    public float CurrentMovingSpeed => currentMovingSpeed;
    public float CurrentAttackSpeed => currentAttackSpeed;
    public float ContactDamage => contactDamage;
    public float AttackDamage => attackDamage;
    public PlayerTargetProvider Target => target;

    public event Action EnemyDeath;

    public EnemyModel(EnemyConfig config)
    {
        cost = config.Cost;
        name = config.EnemyName;
        maxHealth = config.BaseMaxHealth;
        currentHealth = maxHealth;
        baseMovingSpeed = config.BaseMovingSpeed;
        currentMovingSpeed = baseMovingSpeed;
        contactDamage = config.BaseContactDamage;
        attackDamage = config.BaseAttackDamage;
        baseAttackSpeed = config.BaseAttackSpeed;
        currentAttackSpeed = baseAttackSpeed;
    }

    public void TakeDamage(float decrement)
    {
        currentHealth -= decrement;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            EnemyDeath?.Invoke();

            // lines below kinda belong in OnEnable, but this is not MonoBehaviour
            // i'll move it somewhere, in the object pool Get() perhaps
            // TODO: rework this mess
            currentHealth = maxHealth;
            EnemyDeath = null;
        }
    }

    public void SetTarget(PlayerTargetProvider target)
    {
        this.target = target;
    }
}
