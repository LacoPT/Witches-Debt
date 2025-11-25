using System.Collections;
using UnityEngine;
using Zenject;

/// <summary>
/// Do not use this in an actual game, instead make a new one
/// with a scene context that injects list/array of enemies that can spawn
/// on this stage
/// </summary>
public class TestEnemySpawner : MonoBehaviour
{
    //I need an EnemyController.SetTarget method
    [SerializeField] private EnemyController EnemyPrefab;

    private EnemyRegistry registry;
    private PlayerTargetProvider targetProvider;
    private DiContainer container;
    private bool onCooldown = false;
    private const float  cooldown = 1.5f;
    private const float spawnRadius = 10f;

    [Inject]
    public void Construct(EnemyRegistry registry, PlayerTargetProvider targetProvider, DiContainer container)
    {
        Debug.Log("Injection");
        this.registry = registry;
        this.targetProvider = targetProvider;
        this.container = container;
    }

    public void Update()
    {
        if (onCooldown) return;
        Spawn();
        onCooldown = true;
        StartCoroutine(WaitForCooldown());
    }

    public IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    public void Spawn()
    {
        var enemy = Instantiate(EnemyPrefab).GetComponent<EnemyController>();
        var angle = Random.Range(-Mathf.PI, Mathf.PI);
        var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;
        enemy.SetTarget(targetProvider);
        enemy.transform.position = targetProvider.Position + offset;
        registry.Register(enemy);
        enemy.GetComponent<EnemyModelMB>().EnemyModel.EnemyDeath += () => registry.Unregister(enemy);
    }
}