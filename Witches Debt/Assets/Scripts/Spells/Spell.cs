using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Spell : MonoBehaviour
{
    //TODO: PUBLIC UNPROTECTED FIELDS, FIX !!!
    public SpellData data;
    public SpellConfiguration config;
    public bool isClone = false;

    public UnityEvent OnSpawn;
    public UnityEvent<EnemyHittable> OnHit;
    public UnityEvent OnUpdate;
    public UnityEvent OnDestroy;

    protected virtual void Awake()
    {
        //OnSpawn.Invoke();
    }

    protected virtual void Start()
    {
        OnSpawn.Invoke();
    }

    protected virtual void Update()
    {
        OnUpdate.Invoke();
    }
}