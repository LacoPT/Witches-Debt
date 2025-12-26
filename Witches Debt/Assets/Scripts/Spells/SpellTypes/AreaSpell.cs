using UnityEngine;

public class AreaSpell : Spell
{
    [SerializeField] private SpellDataConfig dataConfig;
    
    protected override void Awake()
    {
        InitializeWithConfig(dataConfig);
        base.Awake();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") return;
        if (collision.TryGetComponent<EnemyHittable>(out var hittable))
        {
            hittable.TakeDamage(data.baseDamage);
        }
    }
}