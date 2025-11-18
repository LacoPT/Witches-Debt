using System.Collections.Generic;
using System.Linq;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// Player and EnemySpawner and maybe some others classes depend on
/// </summary>
public class EnemyRegistry
{
    private readonly List<EnemyController> enemies = new();
    public IReadOnlyList<EnemyController> Enemies => enemies;
    public IEnumerable<Vector3> EnemyPositions => Enemies.Select(enemy => enemy.transform.position);

    public void Register(EnemyController enemy)
    {
        enemies.Add(enemy);
    }
    
    public void Unregister(EnemyController enemy)
    {
        enemies.Remove(enemy);
    }
}