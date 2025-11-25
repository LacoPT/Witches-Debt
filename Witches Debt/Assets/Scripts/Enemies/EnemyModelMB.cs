using UnityEngine;

public class EnemyModelMB : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    private EnemyModel enemyModel;
    public EnemyModel EnemyModel => enemyModel;
    public int Cost => config.Cost;
    public EnemyNames Name => config.EnemyName;
    private void Awake()
    {
        enemyModel = new(config);
    }
}
