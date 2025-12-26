using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SpellLoader : MonoBehaviour
{
    [SerializeField] private SpellCaster casterPrefab;
    private List<GameObject> casters = new();
    private DiContainer container;
    private ModLibrary library;
    private InventoryModel inventoryModel;
    //private SpellConfiguration spellConfiguration;
    [Inject]
    public void Construct(DiContainer container, ModLibrary library, InventoryModel inventoryModel)
    {
        this.container = container;
        this.library = library;
        this.inventoryModel = inventoryModel;
        ClearAllCasters();
        LoadFromInventoryModel();
    }

    public void ClearAllCasters()
    {
        foreach (var caster in casters)
        {
            Destroy(caster);
        }
    }

    public void LoadFromInventoryModel()
    {
        var storages = inventoryModel.SpellsStorages;
        foreach (var (spellType, storage) in storages)
        {
            var spellConfiguration = container.Instantiate<SpellConfiguration>();
            var spellPrefab = GetSpellPrefab(spellType);
            spellConfiguration.Type = spellType;
            spellConfiguration.Prefab =  spellPrefab;
            foreach (var modName in storage.Where(modName => modName is not null))
            {
                spellConfiguration.Mods.Add(library.GetModByName(modName));
            }
            var caster = container.InstantiatePrefabForComponent<SpellCaster>(casterPrefab, transform);
            caster.UpdateConfiguration(spellConfiguration);
            casters.Add(caster.gameObject);
        }
    }

    public void TestLoadDefault()
    {
        var spellConfiguration = container.Instantiate<SpellConfiguration>();
        spellConfiguration.Prefab = GetSpellPrefab(SpellType.Area);
        spellConfiguration.Type = SpellType.Area;
        var caster = container.InstantiatePrefabForComponent<SpellCaster>(casterPrefab, transform);
        spellConfiguration.Mods.Add(new RocketMod());
        spellConfiguration.Mods.Add(new TripleShot());
        caster.UpdateConfiguration(spellConfiguration);
    }
    
    private static GameObject GetSpellPrefab(SpellType type)
    {
        var prefabName = type.ToString();
        return Resources.Load<GameObject>(prefabName);
    }
}