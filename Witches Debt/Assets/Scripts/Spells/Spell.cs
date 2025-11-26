using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Spell : MonoBehaviour
{
    //TODO: PUBLIC UNPROTECTED FIELDS, FIX !!!
    public SpellData Data { get; set; }
    public SpellConfiguration Config { get; set; }
    public bool IsClone { get; set; } = false;

    public UnityEvent spawn;
    public UnityEvent<EnemyHittable> hit;
    public UnityEvent afterUpdate;
    public UnityEvent preDestroy;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        spawn.Invoke();
    }

    protected virtual void Update()
    {
        afterUpdate.Invoke();
    }
}