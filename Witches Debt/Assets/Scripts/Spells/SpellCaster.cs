using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private float TestCastTime = 1.5f;
    [SerializeField] private Spell TestSpellPrefab;
    [SerializeField] private SpellType TestSpellType;
    
    private SpellConfiguration config;
    private Func<Vector2> targetChooseFunction;
    private bool onCooldown = false;
    private EnemyRegistry registry;

    //This is a test method right now, todo: read some shit about zenject
    private void Awake()
    {
       targetChooseFunction = ClosestTarget; 
        config = new SpellConfiguration
        {
            type = TestSpellType
        };
        config.mods.Add(new RocketMod());
        config.mods.Add(new TripleShot());
        UpdateConfiguration(config);
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
            var spell = Instantiate(TestSpellPrefab,
                transform.position,
                Quaternion.LookRotation(Vector3.forward, targetChooseFunction()));
            config.ApplyMods(spell);
            onCooldown = true;
            StartCoroutine(WaitForCooldown());
        }
    }

    public void UpdateConfiguration(SpellConfiguration config)
    {
        this.config = config;
    }

    private IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(TestCastTime);
        onCooldown = false;
    }
    
    private static Vector2 RandomAngle()
    {
        var angle = Random.Range(0f, Mathf.PI * 2f);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private Vector2 ClosestTarget()
    {
        Vector3 closest = registry.EnemyPositions.Aggregate((best, p) =>
                (p - transform.position).sqrMagnitude < (best - transform.position).sqrMagnitude ? p : best);
        return (closest - transform.position).normalized;
    }
}
