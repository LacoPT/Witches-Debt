using UnityEngine;

[RequireComponent (typeof(EnemyModelMB))]
public class EnemyHittable : MonoBehaviour
{
    private EnemyModelMB model;
    private void Start()
    {
        model = GetComponent<EnemyModelMB>();
    }

    public void TakeDamage(float damage) => model.TakeDamage(damage);
}