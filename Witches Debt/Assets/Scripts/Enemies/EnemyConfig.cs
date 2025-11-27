using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private EnemyNames enemyName;
    [SerializeField] private int cost;
    [SerializeField] private float baseMaxHP;
    [SerializeField] private float baseMovingSpeed;
    [SerializeField] private float baseContactDamage;
    // stats below are 0 if enemy has no attacks
    [SerializeField] private float baseAttackSpeed;
    [SerializeField] private float baseAttackDamage;

    public EnemyNames EnemyName => enemyName;
    public int Cost => cost;
    public float BaseMaxHealth => baseMaxHP;
    public float BaseMovingSpeed => baseMovingSpeed;
    public float BaseContactDamage => baseContactDamage;
    public float BaseAttackDamage => baseAttackDamage;
    public float BaseAttackSpeed => baseAttackSpeed;
}
