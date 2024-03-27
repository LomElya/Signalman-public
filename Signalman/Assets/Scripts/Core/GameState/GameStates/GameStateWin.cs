using System.Collections.Generic;
using Zenject;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class GameStateWin : GameState
{
    private const string KeyWin = "Win";

    private GameStateManager _gameStateManager;

    private LevelFactory _levelFactory;
    private Level _level;

    private Timer _timer;

    public GameStateWin(GameStateType gameStateType) : base(gameStateType)
    {
    }

    [Inject]
    private void Construct(GameStateManager gameStateManager, LevelFactory levelFactory, Level level, Timer timer)
    {
        _gameStateManager = gameStateManager;

        _levelFactory = levelFactory;
        _level = level;

        _timer = timer;
    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    public override async UniTask OnEnter(string description = "")
    {
        SaveStatus();

        var command1 = _gameStateManager.CreateSetPausedGameCommand(true);

        await _gameStateManager.ExecuteCommands(command1);

        var resultMenu = await GameResultMenu.Load();

        if (_level.CurrentLevelID >= _levelFactory.MaxCountLevel)
        {
            description = $"Игра закончена.\n Уровень пройден за {_timer.PassedSeconds} секунд";
            await resultMenu.Value.Show(GameResultType.End, OnQuit, OnQuit, description);
        }
        else
            await resultMenu.Value.Show(GameResultType.Win, OnNextLevel, OnQuit, description);

        resultMenu.Dispose();
    }

    private void SaveStatus()
    {
        LevelData data = _levelFactory.LevelDatas[_level.CurrentLevelID - 1].Config.Data;
        int value;
        PlayerExtensions.Load(data.NAME_SCENE + KeyWin, out value);
        PlayerExtensions.Save(data.NAME_SCENE + KeyWin, value + 1);

        PlayerExtensions.SaveLevelSeconds(data.NAME_SCENE, _timer.PassedSeconds);
    }

    private async void OnNextLevel()
    {
        LevelData levelData = _levelFactory.GetData(_level.CurrentLevelID);

        Debug.Log($"ID уровня: {_level.CurrentLevelID}");

        string nameScene = levelData.NAME_SCENE;

        var command = _gameStateManager.CreateLoadSceneCommand(nameScene);
        var command1 = _gameStateManager.CreateSetGameStateCommand(GameStateType.GameStart);

        var commands = new List<Command>()
        {
            command,
            command1,
        };

        await _gameStateManager.ExecuteCommands(commands);
    }

    private async void OnQuit()
    {
        var command1 = _gameStateManager.CreateQuitGameCommand();

        await _gameStateManager.ExecuteCommands(command1);
    }
}
