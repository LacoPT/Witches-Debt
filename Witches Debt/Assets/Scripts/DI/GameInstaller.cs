using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EnemyRegistry>().AsSingle();
        Container.Bind<PlayerTargetProvider>().AsSingle();
        Container.Bind<ModLibrary>().AsSingle();
    }
}