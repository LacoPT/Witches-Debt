using System;
using System.Collections;
using System.Collections.Generic;
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
        WaitForLifeTime();
    }

    protected void InitializeWithConfig(SpellDataConfig config)
    {
        data.speed =  config.DefaultSpeed;
        data.baseDamage =  config.DefaultDamage;
        data.size =  config.DefaultScale;
        data.lifeTime =  config.DefaultLifeTime;
        StartCoroutine(WaitForLifeTime());
    }

    private IEnumerator WaitForLifeTime()
    {
        yield return new WaitForSeconds(data.lifeTime);
        Destroy(gameObject);
    }

    protected virtual void Start()
    {
        Spawn.Invoke();
    }

    protected virtual void Update()
    {
        AfterUpdate.Invoke();
    }

    protected virtual void OnDestroy()
    {
        PreDestroy.Invoke();
    }
}