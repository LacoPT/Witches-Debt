using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class EnemyModelMB : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;

    private float maxHealth;
    private float currentHealth;

    public EnemyNames Name => config.EnemyName;
    public int Cost => config.Cost;
    public float CurrentHealth => currentHealth;

    public PlayerTargetProvider Target { get; private set; }
    public float MovingSpeed { get; private set; }
    public float ContactDamage { get; private set; }
    public float AttackDamage { get; private set; }
    public float AttackSpeed { get; private set; }

    public UnityEvent EnemyDeath;

    private void OnEnable()
    {
        maxHealth = config.BaseMaxHealth;
        currentHealth = maxHealth;
        MovingSpeed = config.BaseMovingSpeed;
        ContactDamage = config.BaseContactDamage;
        AttackDamage = config.BaseAttackDamage;
        AttackSpeed = config.BaseAttackSpeed;
    }

    private void OnDisable()
    {
        EnemyDeath.RemoveAllListeners();
    }

    public void Initialize(PlayerTargetProvider target, EnemyStatsScaler scaler, Vector2 position)
    {
        SetTarget(target);
        ScaleStats(scaler);
        SetPosition(position);
    }

    public void SetTarget(PlayerTargetProvider target)
    {
        Target = target;
    }

    private void ScaleStats(EnemyStatsScaler scaler)
    {
        maxHealth *= scaler.HealthModifier;
        currentHealth = maxHealth;
        MovingSpeed *= scaler.MovingSpeedModifier;
        ContactDamage *= scaler.DamageModifier;
        AttackDamage *= scaler.DamageModifier;
        AttackSpeed *= scaler.AttackSpeedModifier;
    }

    private void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void TakeDamage(float decrement)
    {
        currentHealth -= decrement;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            EnemyDeath?.Invoke();
        }
    }
}
