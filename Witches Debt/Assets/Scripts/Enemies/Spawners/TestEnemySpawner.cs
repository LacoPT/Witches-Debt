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

    [Inject]
    public void Construct(EnemyRegistry registry, PlayerTargetProvider targetProvider, DiContainer container)
    {
        this.registry = registry;
        this.targetProvider = targetProvider;
        this.container = container;
    }

    //TODO: Finish this
    public void Spawn()
    {
        //registry.Register(enemy);
    }
}