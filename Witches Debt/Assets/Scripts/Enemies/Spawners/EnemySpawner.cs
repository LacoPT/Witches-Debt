using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

// works as planned, but it's really ugly
// TODO: refactor this shit
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyModelMB> EnemyPrefabs;
    [SerializeField] private List<EnemyNames> EnemyNames;
    [SerializeField] private int coins = 10;
    private EnemyRegistry registry;
    private PlayerTargetProvider targetProvider;
    private DiContainer container;
    private Dictionary<EnemyNames, ObjectPool<EnemyModelMB>> enemyPools = new();
    private bool onCooldown = false;
    private const float cooldown = 2.5f;
    private const float spawnRadius = 10f;

    [Inject]
    public void Construct(EnemyRegistry registry, PlayerTargetProvider targetProvider, DiContainer container)
    {
        Debug.Log("Injection");
        this.registry = registry;
        this.targetProvider = targetProvider;
        this.container = container; // idk what is this
    }

    private void Awake()
    {
        StartCoroutine(WaitForCooldown());
        for (var i = 0; i < EnemyNames.Count; i++)
        {
            enemyPools[EnemyNames[i]] = new EnemyPool(EnemyPrefabs[i], registry).Pool;
        }
    }

    public void Update()
    {
        if (onCooldown) return;
        Spawn();
        StartCoroutine(WaitForCooldown());
    }

    public IEnumerator WaitForCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    public void Spawn()
    {
        var prefab = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)];
        var cnt = coins / prefab.Cost;
        var current = prefab.Name;
        for (var i = 0; i < cnt; i++)
        {
            var enemy = enemyPools[current].Get();
            var angle = Random.Range(-Mathf.PI, Mathf.PI);
            var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;
            enemy.EnemyModel.SetTarget(targetProvider);
            enemy.transform.position = targetProvider.Position + offset;
            enemy.EnemyModel.EnemyDeath += () =>
            {
                enemyPools[enemy.EnemyModel.Name].Release(enemy);
            };
        }
    }
}