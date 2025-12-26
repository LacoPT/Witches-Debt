using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameContext : MonoInstaller
{
    [SerializeField] private ModRarityDistribution modRarityDistribution;
    public override void InstallBindings()
    {
        Container.Bind<PlayerTargetProvider>().AsSingle();
        Container.Bind<PlayerControls>().AsSingle();
        Container.Bind<ModLibrary>().AsSingle().WithArguments(modRarityDistribution, Container);
        Container.Bind<SpellConfiguration>().AsSingle();
        Container.Bind<EnemyRegistry>().AsSingle();
        Container.Bind<StatusEffectLibrary>().AsSingle();
    }
}