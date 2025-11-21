using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ModRarityDistribution modRarityDistribution;
    public override void InstallBindings()
    {
        Container.Bind<EnemyRegistry>().AsSingle();
        Container.Bind<PlayerTargetProvider>().AsSingle();
        Container.Bind<ModLibrary>().AsSingle().WithArguments(modRarityDistribution);
    }
}