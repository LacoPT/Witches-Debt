using System;
using UnityEngine;

public class ShotSpell : Spell
{
    [SerializeField] private float testSpeed = 15f;

    protected override void Awake()
    {
        base.Awake();
        data.speed = testSpeed;
    }

    protected override void Update()
    {
        base.Update();
        transform.position += data.speed * Time.deltaTime * transform.up;
    }
}
