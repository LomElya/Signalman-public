using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class Gameplay : MonoBehaviour, IGameModeCleaner
{
    public string SceneName => Constants.Scenes.GAMESCENE;

    private GameState _currentGameState;
    private GameStateFabric _fabric;

    private AssetProvider _assetProvider;

    private Dictionary<GameStateType, GameState> _states = new();

    public GameStateType PrevGameState { get; private set; }
    public SceneInstance GameScene { get; private set; }

    private PlayerFactory _playerFactory;


    public IEnumerable<GameObjectFactory> Factories => new GameObjectFactory[]
        {
           _playerFactory
        };

    [Inject]
    private void Construct(GameStateFabric fabric, AssetProvider assetProvider, PlayerFactory playerFactory)
    {
        _fabric = fabric;
        _assetProvider = assetProvider;
        _playerFactory = playerFactory;
    }

    public async UniTask Init()
    {
        _currentGameState = _fabric.CreateGameState(GameStateType.GameStart);
        PrevGameState = GameStateType.GameStart;
        await _currentGameState.OnEnter();
    }

    public async UniTask SetState(GameStateType gameStateType, string description = "")
    {
        if (!_currentGameState.CanSwitchToState(gameStateType))
            return;

        GameState newGameState;

        if (_states.ContainsKey(gameStateType))
            newGameState = _states[gameStateType];
        else
        {
            newGameState = _fabric.CreateGameState(gameStateType);
            _states.Add(gameStateType, newGameState);
        }

        PrevGameState = _currentGameState.GameStateType;
        _currentGameState = newGameState;

        await newGameState.OnEnter(description);
    }

    public async UniTask SetGameScene(string nameScene) => GameScene = await _assetProvider.LoadSceneAdditive(nameScene);
    public async UniTask Cleanup() => await _assetProvider.UnloadAdditiveScene(GameScene);
}
