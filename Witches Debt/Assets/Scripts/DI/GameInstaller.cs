using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ModRarityDistribution modRarityDistribution;
    public override void InstallBindings()
    {
        Container.Bind<PlayerTargetProvider>().AsSingle();
        Container.Bind<SpellConfiguration>().AsSingle();
        Container.Bind<EnemyRegistry>().AsSingle();
    }
}