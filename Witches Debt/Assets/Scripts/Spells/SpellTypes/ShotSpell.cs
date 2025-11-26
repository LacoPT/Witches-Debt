using System;
using UnityEngine;

public class ShotSpell : Spell
{
    [SerializeField] private float testSpeed = 15f;

    protected override void Awake()
    {
        base.Awake();
        Data.speed = testSpeed;
    }

    protected override void Update()
    {
        base.Update();
        transform.position += Data.speed * Time.deltaTime * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") return;
        if (collision.TryGetComponent<EnemyHittable>(out var hittable))
        {
            //TODO: Introduce a field with damage, or make a AttackData with effects that are being applied
            hittable.TakeDamage(Data.baseDamage);
        }
        Destroy(gameObject);
    }
}
