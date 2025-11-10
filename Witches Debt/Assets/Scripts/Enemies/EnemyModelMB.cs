using UnityEngine;

public class EnemyModelMB : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    private EnemyModel enemyModel;
    public EnemyModel EnemyModel => enemyModel;

    private void Awake()
    {
        enemyModel = new(config);
    }
}
