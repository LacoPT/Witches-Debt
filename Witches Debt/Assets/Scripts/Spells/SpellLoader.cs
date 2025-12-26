using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpellLoader : MonoBehaviour
{
    [SerializeField] private SpellCaster casterPrefab;
    private List<GameObject> casters = new();
    private DiContainer container;
    private ModLibrary library;
    //private SpellConfiguration spellConfiguration;
    //[Inject]
    //public void Construct(DiContainer container, ModLibrary library)
    //{
    //    this.container = container;
    //    this.library = library;
    //}

    private void Awake()
    {
        container = ProjectContext.Instance.Container;
        library = container.Resolve<ModLibrary>();
    }

    public void ClearAllCasters()
    {
        foreach (var caster in casters)
        {
            Destroy(caster);
        }
    }

    //public void LoadFromSaveData(SpellConfigurationSaveData saveData)
    //{
    //    var spellConfiguration = container.Instantiate<SpellConfiguration>();
    //    spellConfiguration.FromSaveData(saveData);
    //    //var caster = Instantiate(casterPrefab, transform);
    //    var caster = container.InstantiatePrefabForComponent<SpellCaster>(casterPrefab, transform);
    //    caster.UpdateConfiguration(spellConfiguration);
    //}

    public void LoadFromInventoryModel(InventoryModel inventoryModel)
    {
        Debug.Log($"Library is null: {library == null}");
        var storages = inventoryModel.SpellsStorages;
        foreach (var storageKv in storages)
        {
            var spellType = storageKv.Key;
            var storage = storageKv.Value;
            var spellConfiguration = container.Instantiate<SpellConfiguration>();
            var spellPrefab = GetSpellPrefab(spellType);
            spellConfiguration.Prefab = spellPrefab;
            foreach (var modName in storage)
            {
                if (modName is null) continue;
                spellConfiguration.mods.Add(library.GetModByName(modName));
            }
            //var caster = container.InstantiatePrefabForComponent<SpellCaster>(casterPrefab, transform);
            var caster = Instantiate(casterPrefab, transform);
            caster.UpdateConfiguration(spellConfiguration);
            casters.Add(caster.gameObject);
        }
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