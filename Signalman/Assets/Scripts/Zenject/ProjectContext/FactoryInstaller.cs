using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    [SerializeField] private PlayerFactory _playerFactory;
    [SerializeField] private LevelFactory _levelFactory;

    public override void InstallBindings()
    {
        BindPlayerFactory();
        BindLevelFactory();
    }

    private void BindPlayerFactory()
    {
        Container.BindInterfacesAndSelfTo<PlayerFactory>().FromInstance(_playerFactory).AsSingle();
    }

    private void BindLevelFactory()
    {
        Container.BindInterfacesAndSelfTo<LevelFactory>().FromInstance(_levelFactory).AsSingle();
    }
}
