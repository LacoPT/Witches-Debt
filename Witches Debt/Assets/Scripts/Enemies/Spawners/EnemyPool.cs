using UnityEngine.Pool;
using Object = UnityEngine.Object;
public class EnemyPool
{
    public ObjectPool<EnemyModelMB> Pool { get; private set; }
    private readonly EnemyModelMB prefab;
    private EnemyRegistry registry;
    //private EnemyStatsScale scale;

    public EnemyPool(EnemyModelMB prefab, EnemyRegistry registry)
    {
        this.prefab = prefab;
        this.registry = registry; // idk if this could be replaced with inject
        //this.scale = scale;
        Pool = new ObjectPool<EnemyModelMB>
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

    private EnemyModelMB CreateEnemy()
    {
        var e = Object.Instantiate(prefab);
        return e;
    }

    private void OnGetEnemy(EnemyModelMB e)
    {
        e.gameObject.SetActive(true);
        registry.Register(e);
    }

    private void OnEnemyRelease(EnemyModelMB e)
    {
        registry.Unregister(e);
        e.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(EnemyModelMB e)
    {
        Object.Destroy(e.gameObject);
    }
}