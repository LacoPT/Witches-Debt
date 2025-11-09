using UnityEngine;

[RequireComponent (typeof(EnemyModelMB))]
public class EnemyHittable : MonoBehaviour
{
    private EnemyModel model;
    private void Start()
    {
        model = GetComponent<EnemyModelMB>().EnemyModel;
    }

    public void TakeDamage(float damage) => model.TakeDamage(damage);
}