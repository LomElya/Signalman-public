using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

public class GameStateStart : GameState
{
    private GameStateManager _gameStateManager;

    private LevelFactory _levelFactory;
    private Level _level;

    public GameStateStart(GameStateType gameStateType) : base(gameStateType)
    {
    }

    [Inject]
    private void Construct(GameStateManager gameStateManager, LevelFactory levelFactory, Level level)
    {
        _gameStateManager = gameStateManager;

        _levelFactory = levelFactory;
        _level = level;
    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    public override async UniTask OnEnter(string description = "")
    {
        LevelData levelData = _levelFactory.GetData(_level.CurrentLevelID);

        Debug.Log($"На сцене {levelData.NAME_SCENE} таймер {levelData.MaxTime} секунд");

        var command1 = _gameStateManager.CreateStartGameCommand(levelData);
        var command2 = _gameStateManager.CreateSetPausedGameCommand(false);
        var command3 = _gameStateManager.CreateSetGameStateCommand(GameStateType.GameInProgress);

        var commands = new List<Command>()
        {
            command1,
            command2,
            command3,
        };

        await _gameStateManager.ExecuteCommands(commands);
    }
}
