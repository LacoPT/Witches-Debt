using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ModRarityDistribution modRarityDistribution;
    public override void InstallBindings()
    {
        Container.Bind<PlayerControls>().AsSingle();
        Container.Bind<ModLibrary>().AsSingle().WithArguments(modRarityDistribution);
        Container.Bind<PlayerStats>().AsSingle();
        Container.Bind<InventoryModel>().AsSingle();
    }
}