using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private EnemyNames enemyName;
    [SerializeField] private int cost;
    [SerializeField] private float maxHP;
    [SerializeField] private float baseMovingSpeed;
    [SerializeField] private float contactDamage;
    // stats below are 0 if enemy has no attacks
    [SerializeField] private float baseAttackSpeed;
    [SerializeField] private float attackDamage;

    public EnemyNames EnemyName => enemyName;
    public int Cost => cost;
    public float MaxHealth => maxHP;
    public float BaseMovingSpeed => baseMovingSpeed;
    public float ContactDamage => contactDamage;
    public float AttackDamage => attackDamage;
    public float BaseAttackSpeed => baseAttackSpeed;
}
