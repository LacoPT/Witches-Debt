using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Spell : MonoBehaviour
{
    //TODO: PUBLIC UNPROTECTED FIELDS, FIX !!!
    public SpellData data;
    public SpellConfiguration config { get; set; }
    public bool isClone { get; set; } = false;

    public UnityEvent Spawn;
    public UnityEvent<EnemyHittable> Hit;
    public UnityEvent AfterUpdate;
    public UnityEvent PreDestroy;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        Spawn.Invoke();
    }

    protected virtual void Update()
    {
        AfterUpdate.Invoke();
    }
}