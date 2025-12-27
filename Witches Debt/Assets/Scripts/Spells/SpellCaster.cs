using System;
using System.Collections;
using System.Linq;
using ModestTree;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

public class SpellCaster : MonoBehaviour
{
    private const float DefaultCastTime = 1.5f;
    private AudioSource source;
    [SerializeField] private SpellPrefabConfig testSpellPrefabConfig;
    
    private SpellConfiguration config;
    private Func<Vector2> shootDirectionFunc;
    private bool onCooldown = false;
    private EnemyRegistry registry;
    private TargetSelectType selectType = TargetSelectType.Direction;
    public UnityEvent<SpellType> SpellCasted;

    private void Awake()
    {
        shootDirectionFunc = ClosestTarget;
    }

    [Inject]
    public void Contruct(EnemyRegistry registry)
    {
        this.registry = registry;
    }

    private void Update()
    {
        if (!onCooldown)
        {
            SpawnSpell();
        }
    }

    private void SpawnSpell()
    {
        var pos = selectType switch
        {
            TargetSelectType.Direction => (Vector2)transform.position,
            TargetSelectType.Point => shootDirectionFunc(),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        
        var spellObject = Instantiate(config.Prefab,
            pos,
            Quaternion.LookRotation(Vector3.forward, shootDirectionFunc()));
        var spell = spellObject.GetComponent<Spell>();
        config.ApplyMods(spell);
        onCooldown = true;
        SpellCasted?.Invoke(SpellType.Shot);
        StartCoroutine(WaitForCooldown());
    }

    public void UpdateConfiguration(SpellConfiguration config)
    {
        this.config = config;
        selectType = config.Type == SpellType.Area ? TargetSelectType.Point : TargetSelectType.Direction;
    }

    private IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(DefaultCastTime);
        onCooldown = false;
    }
    
    private Vector2 RandomAngle()
    {
        var angle = Random.Range(0f, Mathf.PI * 2f);
        if (selectType == TargetSelectType.Point)
            //TODO: Move to const/config
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 3 + (Vector2)transform.position;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private Vector2 ClosestTarget()
    {
        if (registry.Enemies.IsEmpty()) return RandomAngle();
        Vector3 closest = registry.EnemyPositions.Aggregate((best, p) =>
                (p - transform.position).sqrMagnitude < (best - transform.position).sqrMagnitude ? p : best);
        if(selectType == TargetSelectType.Point)
            //TODO: move to const/config
            return (closest - transform.position).sqrMagnitude < 150 ? closest : RandomAngle();
        return (closest - transform.position).normalized;
    }
}
