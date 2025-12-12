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

    public void LoadFromSaveData(SpellConfigurationSaveData saveData)
    {
        var spellConfiguration = container.Instantiate<SpellConfiguration>();
        spellConfiguration.FromSaveData(saveData);
        var caster = Instantiate(casterPrefab, transform);
        caster.UpdateConfiguration(spellConfiguration);
    }
}