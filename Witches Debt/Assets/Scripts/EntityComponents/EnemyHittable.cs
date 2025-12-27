using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent (typeof(EnemyModelMB))]
public class EnemyHittable : MonoBehaviour
{
    private EnemyModelMB model;
    private DotHelper dotHelper;
    private Dictionary<StatusEffect, int> Stacks = new();
    private bool needClear = false;

    private void Start()
    {
        model = GetComponent<EnemyModelMB>();
        dotHelper = model.DotHelper;
        model.EnemyDeath.AddListener(() =>
        {
            needClear = true;
            dotHelper.UnregisterForDot(this);
        });
    }

    public void TakeDamage(float damage) => model.TakeDamage(damage);

    public void ApplyEffect(StatusEffect status)
    {
        if (Stacks.TryAdd(status, 1))
        {
            if(status.StatusEffectType == StatusEffectType.Dot)
                dotHelper.RegisterForDot(this);
            model.EnemyDeath.AddListener(() =>
            {
            });
        }
        else
        {
            Stacks[status]++;
        }
    }

    public void DotTick()
    {
        if (needClear)
        {
            needClear = false;
            Stacks.Clear();
        }
        
        foreach (var (status, stack) in Stacks)
        {
            var damageEffect = status as IDamageEffect;
            if (damageEffect == null || status.StatusEffectType != StatusEffectType.Dot || stack == 0) return;
            var dmg = damageEffect.GetDamage(model, stack);
            TakeDamage(dmg);
            //Debug.Log($"Damage from effect is taken! Damage: {dmg}, effect: {status},  stack: {stack}, hp: {model.CurrentHealth}");
        }
    }
}