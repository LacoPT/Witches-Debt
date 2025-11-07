using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private float TestCastTime = 1.5f;
    [SerializeField] private Spell TestSpellPrefab;
    [SerializeField] private SpellType TestSpellType;
    
    private SpellConfiguration config;
    private Func<Vector2> targetChooseFunction = RandomAngle;
    private bool onCooldown = false;

    //This is a test method right now, todo: read some shit about zenject
    private void Awake()
    {
        config = new SpellConfiguration
        {
            type = TestSpellType
        };
        config.mods.Add(new RocketMod());
        config.mods.Add(new TripleShot());
        UpdateConfiguration(config);
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
}
