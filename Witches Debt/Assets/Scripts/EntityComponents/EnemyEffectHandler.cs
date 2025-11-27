using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyModelMB))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EnemyHittable))]
public class EnemyEffectHandler : MonoBehaviour
{
    public EnemyModelMB model { get; private set; }
    public SpriteRenderer renderer { get; private set; }
    
    private Dictionary<StatusEffect, int> EffectStacks = new();
}
