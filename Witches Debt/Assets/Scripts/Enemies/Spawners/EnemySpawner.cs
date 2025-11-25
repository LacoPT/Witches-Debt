using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

// works as planned, but it's really ugly
// TODO: refactor this shit
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyController> EnemyPrefabs;
    [SerializeField] private List<EnemyNames> EnemyNames;
    private Dictionary<EnemyNames, ObjectPool<EnemyController>> objectPools = new();
    private EnemyRegistry registry;
    private PlayerTargetProvider targetProvider;
    private DiContainer container;
    private bool onCooldown = false;
    private const float cooldown = 2.5f;
    private const float spawnRadius = 10f;
    private int coins = 10;
    private EnemyNames current;
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
            current = (EnemyNames) i;
            objectPools[EnemyNames[i]] = new ObjectPool<EnemyController>
            (
                createFunc: CreateEnemy,
                actionOnGet: OnGetEnemy,
                actionOnRelease: OnEnemyRelease,
                actionOnDestroy: OnDestroyEnemy,
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 50
            );
        }
    }

    private EnemyController CreateEnemy()
    {
        var e = Instantiate(EnemyPrefabs[(int)current]);
        e.gameObject.SetActive(false);
        return e;
    }

    private void OnGetEnemy(EnemyController e)
    {
        e.gameObject.SetActive(true);
    }

    private void OnEnemyRelease(EnemyController e)
    {
        e.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(EnemyController e)
    {
        Destroy(e.gameObject);
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
        var rand = Random.Range(0, EnemyPrefabs.Count);
        var prefab = EnemyPrefabs[rand];
        var modelMB = prefab.GetComponent<EnemyModelMB>();
        var cnt = coins / modelMB.Cost;
        current = modelMB.Name;
        for (var i = 0; i < cnt; i++)
        {
            var enemy = objectPools[current].Get();
            var angle = Random.Range(-Mathf.PI, Mathf.PI);
            var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;
            enemy.SetTarget(targetProvider);
            enemy.transform.position = targetProvider.Position + offset;
            registry.Register(enemy);
            var mb = enemy.GetComponent<EnemyModelMB>();
            mb.EnemyModel.EnemyDeath += () =>
            {
                registry.Unregister(enemy);
                objectPools[mb.Name].Release(enemy);
            };
        }
    }
}