using System.Collections.Generic;
using System.Linq;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// Player and EnemySpawner and maybe some others classes depend on
/// </summary>
public class EnemyRegistry
{
    private readonly List<EnemyModelMB> enemies = new();
    public IReadOnlyList<EnemyModelMB> Enemies => enemies;
    public IEnumerable<Vector3> EnemyPositions => Enemies.Select(enemy => enemy.transform.position);

    public void Register(EnemyModelMB enemy)
    {
        enemies.Add(enemy);
    }
    
    public void Unregister(EnemyModelMB enemy)
    {
        enemies.Remove(enemy);
    }
}