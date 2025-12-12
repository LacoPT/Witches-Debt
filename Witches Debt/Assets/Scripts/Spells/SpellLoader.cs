using UnityEngine;
using Zenject;

public class SpellLoader : MonoBehaviour
{
    [SerializeField] private SpellCaster casterPrefab;
    private DiContainer container;

    [Inject]
    public void Construct(DiContainer container)
    {
        this.container = container;
    }

    private void Awake()
    {
        TestLoadDefault();
    }

    public void LoadFromSaveData(SpellConfigurationSaveData saveData)
    {
        var spellConfiguration = container.Instantiate<SpellConfiguration>();
        spellConfiguration.FromSaveData(saveData);
        //var caster = Instantiate(casterPrefab, transform);
        var caster = container.InstantiatePrefabForComponent<SpellCaster>(casterPrefab, transform);
        caster.UpdateConfiguration(spellConfiguration);
    }

    public void TestLoadDefault()
    {
        var spellConfiguration = container.Instantiate<SpellConfiguration>();
        //spellConfiguration.PrefabConfig = SpellPrefabConfig.;
        spellConfiguration.Prefab = GetSpellPrefab(SpellType.Shot);
        var caster = container.InstantiatePrefabForComponent<SpellCaster>(casterPrefab, transform);
        spellConfiguration.mods.Add(new RocketMod());
        spellConfiguration.mods.Add(new TripleShot());
        caster.UpdateConfiguration(spellConfiguration);
    }
    
    public GameObject GetSpellPrefab(SpellType type)
    {
        var prefabName = type.ToString();
        return Resources.Load<GameObject>(prefabName);
    }
}