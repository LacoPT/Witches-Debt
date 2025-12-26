using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent (typeof(EnemyModelMB))]
public class EnemyHittable : MonoBehaviour
{
    private EnemyModelMB model;
    private DotHelper dotHelper;
    private Dictionary<StatusEffect, int> Stacks = new();

    [Inject]
    public void Construct(DotHelper dot)
    {
        this.dotHelper = dot;
    }
    private void Start()
    {
        model = GetComponent<EnemyModelMB>();
    }

    public void TakeDamage(float damage) => model.TakeDamage(damage);

    public void ApplyEffect(StatusEffect status)
    {
    }

    public void DotTick()
    {
        foreach (var (status, stack) in Stacks)
        {
            var damageEffect = status as IDamageEffect;
            if (damageEffect == null || status.StatusEffectType != StatusEffectType.Dot || stack == 0) return;
            var dmg = damageEffect.GetDamage(model, stack);
            TakeDamage(dmg);
            Debug.Log($"Damage from effect is taken! Damage: {dmg}, effect: {status},  stack: {stack}, hp: {model.CurrentHealth}");
        }
    }

    private void OnDestroy()
    {
        dotHelper.UnregisterForDot(this);
    }
}