using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerTargetProvider>().AsSingle();
        Container.Bind<PlayerControls>().AsSingle();
    }
}