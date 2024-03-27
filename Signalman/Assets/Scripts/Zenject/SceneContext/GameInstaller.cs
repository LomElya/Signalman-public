using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Gameplay _gameplay;
    [SerializeField] private GameProcessor _gameProcessor;

    public override void InstallBindings()
    {
        //   BindSignals();

        Container.BindInstance(_gameProcessor);

        BindGameplay();
    }

    private void BindGameplay()
    {
        Container.BindInterfacesAndSelfTo<Gameplay>().FromInstance(_gameplay).AsSingle();

        var gameStateFabric = new GameStateFabric();
        Container.BindInstance(gameStateFabric);
        Container.QueueForInject(gameStateFabric);

        var turnManager = new GameStateManager(this);
        Container.Bind<GameStateManager>().FromInstance(turnManager).AsSingle();
        Container.QueueForInject(turnManager);

        Container.BindInterfacesAndSelfTo<PlayerStateController>().AsSingle();

        var playerStateFabric = new PlayerStateFabric();
        Container.BindInstance(playerStateFabric);
        Container.QueueForInject(playerStateFabric);
    }

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameLoseSignal>().OptionalSubscriber();
        Container.DeclareSignal<PauseSignal>().OptionalSubscriber();
    }
}
