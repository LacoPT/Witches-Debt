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
    [SerializeField] private float TestCastTime = 1.5f;
    private AudioSource source; // TODO: move to separate class
    //[SerializeField] private Spell TestSpellPrefab;
    //[SerializeField] private SpellPrefabConfig testSpellPrefabConfig;
    
    private SpellConfiguration config;
    private Func<Vector2> shootDirectionFunc;
    private bool onCooldown = false;
    private EnemyRegistry registry;
    public UnityEvent<SpellType> SpellCasted;

    //This is a test method right now
    //TODO: make a spell build system with ui
    private void Awake()
    {
       shootDirectionFunc = ClosestTarget; 
        //config = new SpellConfiguration
        //{
            //PrefabConfig = testSpellPrefabConfig
        //};
        //config.mods.Add(new RocketMod());
        //config.mods.Add(new TripleShot());
        //UpdateConfiguration(config);
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
        var spellObject = Instantiate(config.Prefab,
            transform.position,
            Quaternion.LookRotation(Vector3.forward, shootDirectionFunc()));
        var spell = spellObject.GetComponent<Spell>();
        config.ApplyMods(spell);
        onCooldown = true;
        SpellCasted?.Invoke(TestSpellType);
        StartCoroutine(WaitForCooldown());
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
        if (registry.Enemies.IsEmpty()) return RandomAngle();
        Vector3 closest = registry.EnemyPositions.Aggregate((best, p) =>
                (p - transform.position).sqrMagnitude < (best - transform.position).sqrMagnitude ? p : best);
        return (closest - transform.position).normalized;
    }
}
