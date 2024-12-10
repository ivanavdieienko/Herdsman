using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private GameConfig gameConfig;

    [SerializeField]
    private MainHero hero;

    [SerializeField]
    private Animal animalPrefab;

    public override void InstallBindings()
    {
        Container.Bind<GameConfig>().FromInstance(gameConfig).AsSingle();

        Container.Bind<MainHero>().FromInstance(hero).AsSingle();

        Container.Bind<UserData>().AsSingle();

        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<AnimalDetectedSignal>();
        Container.DeclareSignal<AnimalDeliveredSignal>();
        Container.DeclareSignal<ResetScoreSignal>();

        Container.Bind<AddScoreCommand>().AsTransient();
        Container.Bind<ResetScoreCommand>().AsTransient();

        Container.BindSignal<AnimalDeliveredSignal>()
            .ToMethod<AddScoreCommand>((cmd, s) => cmd.Execute()).FromResolve();

        Container.BindSignal<ResetScoreSignal>()
            .ToMethod<ResetScoreCommand>((cmd, s) => cmd.Execute()).FromResolve();

        Container.BindInterfacesAndSelfTo<AnimalGroupManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<AnimalSpawner>().AsSingle();

        Container.BindMemoryPool<Animal, Animal.Pool>()
            .WithInitialSize(gameConfig.AnimalPoolSize)
            .FromComponentInNewPrefab(animalPrefab)
            .UnderTransformGroup("Animals");
    }
}
